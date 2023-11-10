using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MinionManager : CharacterBase
{
    MinionState _minionState = MinionState.Moving;

    protected override void DeadCharacter()
    {
    }

    public override IEnumerator KnockUp(float sec)
    {
        // TO:DO ~‚Ü‚éˆ—
        _minionState = MinionState.airborne;
        _body.DOJump(_body.position, sec, 1, sec); // minion‚Ìanimation‚ğg‚¤ê‡•ÏX•K{
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