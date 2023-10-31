using UnityEngine;
using System.Collections;
using System;

public class ChampionManager : CharacterBase
{
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
    public ChampionAnimationCntlr ChampAnimContlr { get => _champAnimContlr; }
    PlayerMove _playerMove;
    ChampionState _champState = ChampionState.Idle;
    public ChampionState ChampState { get => _champState; set => _champState = value; }

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    /// <summary>Range内かチェック</summary>
    /// <param name="target"></param>
    public IEnumerator TargetDesignationCheck(float Range, Action action)
    {
        if (!_designatedObject) yield break;
        if (Range * 0.02f < (_designatedObject.This2DPos() - this.This2DPos()).magnitude) // Range外
        {
            _playerMove.MoveToDesignatTarget(_designatedObject);
            yield return null;
            StartCoroutine(TargetDesignationCheck(Range, action));
        }
        else
        {
            _playerMove.StopMove();
            _playerMove.SetForward(_designatedObject.transform.position - this.transform.position);
            action?.Invoke();
            yield break;
        }
    }

    protected override void DeadCharacter()
    {
    }

    public override IEnumerator KnockUp(float sec)
    {
        _playerMove.StopMove();
        _champState = ChampionState.airborne;
        yield return new WaitForSeconds(sec);
        _champState = ChampionState.Idle;
    }
}

public enum ChampionState
{
    Idle,
    Moving,
    AAing,
    channeling,
    airborne,
    dead
}