using UnityEngine;
using System.Collections;
using System;

public class ChampionManager : CharacterBase
{
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
    public ChampionAnimationCntlr ChampAnimContlr { get => _champAnimContlr; }
    //PlayerMove _playerMove;
    PlayerMoveNavMesh _playeMoveNav;
    ChampionState _champState = ChampionState.Idle;
    public ChampionState ChampState
    { 
        get => _champState;
        set
        {
            if (_champState == ChampionState.airborne) return; // ノックアップ中はState変更できない
            _champState = value;
        }
    }

    private void Awake()
    {
        //_playerMove = GetComponent<PlayerMove>();
        _playeMoveNav = GetComponent<PlayerMoveNavMesh>();
    }

    /// <summary>対象指定したobjに向かって移動、Rengeに入ったらActionする</summary>
    /// <param name="target"></param>
    public IEnumerator TargetDesignationCheck(float Range, Action action)
    {
        if (!_designatedObject) yield break;
        if (Range * 0.02f < (_designatedObject.This2DPos() - this.This2DPos()).magnitude) // Range外
        {
            _playeMoveNav.SetDestination(_designatedObject.transform.position);
            yield return null;
            StartCoroutine(TargetDesignationCheck(Range, action));
            yield break;
        }
        else
        {
            _playeMoveNav.StopMove();
            _playeMoveNav.SetForward(_designatedObject.transform.position - this.transform.position);
            action?.Invoke();
            yield break;
        }
    }

    protected override void DeadCharacter()
    {
    }

    public override IEnumerator KnockUp(float sec)
    {
        _playeMoveNav.StopMove();
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