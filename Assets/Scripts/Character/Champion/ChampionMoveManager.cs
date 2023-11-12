using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

/// <summary>�`�����s�I���̓������Ǘ����� </summary>
public class ChampionMoveManager : CharacterMoveManager
{
    PlayerMoveNavMesh _playeMoveNav;
    ChampionState _champState = ChampionState.Idle;
    public ChampionState ChampState
    {
        get => _champState;
        set
        {
            if (_champState == ChampionState.channeling || _champState == ChampionState.airborne || _champState == ChampionState.dead) return; // State�ύX�ł��Ȃ�State
            _champState = value;
        }
    }

    private void Awake()
    {
        _playeMoveNav = GetComponent<PlayerMoveNavMesh>();
    }

    /// <summary>�Ώێw�肵��obj�Ɍ������Ĉړ��ARenge�ɓ�������Action����</summary>
    /// <param name="target"></param>
    public IEnumerator TargetDesignationCheck(float Range, Action action)
    {
        if (!_designatedObject) yield break;
        if (Range * 0.02f < (_designatedObject.MoveManager.This2DPos() - this.This2DPos()).magnitude) // Range�O
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

    public override IEnumerator KnockUp(float sec)
    {
        _playeMoveNav.StopMove();
        _champState = ChampionState.airborne;
        _body.DOJump(_body.position, sec, 1, sec); // knock up �A���ŐH�������o�O�邩��
        yield return new WaitForSeconds(sec);
        _champState = ChampionState.Idle;
    }

    public void FinishChanneling(ChampionState finishState)
    {
        if (_champState == ChampionState.channeling) _champState = finishState;
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