using UnityEngine;
using System.Collections;

public class MinionController : CharacterBase
{
    protected override void DeadCharacter()
    {
    }

    protected override IEnumerator KnockUp(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
