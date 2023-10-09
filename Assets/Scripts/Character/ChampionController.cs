using UnityEngine;

public class ChampionController : CharacterBase
{
    protected override void AA(CharacterBase target)
    {
        DealDamage((int)_charaParam.AD, DamageType.AD, target);
    }

    void AATargetSet(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out CharacterBase characterBase)) // AA�o����obj������
        {
            AA(characterBase);
        }
    }

    protected override void DeadCharacter()
    {
    }

    private void OnEnable() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
