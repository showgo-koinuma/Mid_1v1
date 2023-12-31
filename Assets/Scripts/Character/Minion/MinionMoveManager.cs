using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MinionMoveManager : CharacterMoveManager
{
    MinionState _minionState = MinionState.Moving;

    public override IEnumerator KnockUp(float sec)
    {
        // TO:DO 止まる処理
        _minionState = MinionState.airborne;
        _body.DOJump(_body.position, sec, 1, sec); // minionのanimationを使う場合変更必須
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