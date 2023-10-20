using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MinionController : CharacterBase
{
    MinionState _minionState = MinionState.Moving;

    protected override void DeadCharacter()
    {
    }

    public override IEnumerator KnockUp(float sec)
    {
        _minionState = MinionState.airborne;
        this.transform.DOJump(Vector3.zero, sec, 1, sec);
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