using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MinionMoveManager : CharacterMoveManager
{
    MinionState _minionState = MinionState.Moving;

    public override IEnumerator KnockUp(float sec)
    {
        // TO:DO �~�܂鏈��
        _minionState = MinionState.airborne;
        _body.DOJump(_body.position, sec, 1, sec); // minion��animation���g���ꍇ�ύX�K�{
        yield return new WaitForSeconds(sec);
        _minionState = MinionState.Moving;
    }
}

public enum MinionState
{
    Moving,
    AAing,
    airborne
}