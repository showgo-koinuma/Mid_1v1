using UnityEngine;
using System.Collections;

public class ChampionController : CharacterBase
{
    PlayerController _playerCntlr;

    private void Awake()
    {
        _playerCntlr = GetComponent<PlayerController>();
    }

    protected override void AA(CharacterBase target)
    {
        DealDamage((int)_charaParam.AD, DamageType.AD, target);
    }

    /// <summary>Range�����`�F�b�N</summary>
    /// <param name="target"></param>
    IEnumerator AACheck(CharacterBase target)
    {
        if (_charaParam.Range * 0.02f < (target.This2DPos() - this.This2DPos()).magnitude) // Range�O
        {
            _playerCntlr.SetAATarget(target);
            yield return new WaitForEndOfFrame();
            StartCoroutine(AACheck(target));
        }
        else
        {
            AA(target);
            _playerCntlr.StopMove();
            yield return null;
        }
    }

    /// <summary>AA�̃^�[�Q�b�g���Z�b�g����</summary>
    /// <param name="hit"></param>
    void AATargetSet(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out CharacterBase characterBase)) // AA�o����obj������
        {
            StartCoroutine(AACheck(characterBase));
        }
    }

    protected override void DeadCharacter()
    {
    }

    private void OnEnable() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
