using UnityEngine;
using System.Collections;
using System;

public class ChampionController : CharacterBase
{
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
    public ChampionAnimationCntlr ChampAnimContlr { get => _champAnimContlr; }
    PlayerController _playerCntlr;

    private void Awake()
    {
        _playerCntlr = GetComponent<PlayerController>();
    }

    /// <summary>Range内かチェック</summary>
    /// <param name="target"></param>
    public IEnumerator TargetDesignationCheck(int Range, Action action)
    {
        if (!_designatedObject) yield break;
        if (Range * 0.02f < (_designatedObject.This2DPos() - this.This2DPos()).magnitude) // Range外
        {
            _playerCntlr.DesignatTarget(_designatedObject);
            yield return null;
            StartCoroutine(TargetDesignationCheck(Range, action));
        }
        else
        {
            action?.Invoke();
            _playerCntlr.SetForward(_designatedObject.transform.position - this.transform.position);
            _playerCntlr.StopMove();
            yield break;
        }
    }

    protected override void DeadCharacter()
    {
    }
}
