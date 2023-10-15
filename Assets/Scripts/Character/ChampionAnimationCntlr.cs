using UnityEngine;

/// <summary>�`�����s�I���̃A�j���[�V�����Ǘ�</summary>
public class ChampionAnimationCntlr : MonoBehaviour
{
    Animator _animator;
    ChampionManager _champManager;
    ChampAA _champAA;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _champManager = transform.parent.gameObject.GetComponent<ChampionManager>();
        _champAA = _champManager.gameObject.GetComponent<ChampAA>();
    }

    public void StartAAAnimation() => _animator.SetTrigger("AA");

    public void StartQAnimation() => _animator.SetTrigger("Q");

    public void SetIsMove(bool isMove)
    {
        _animator.SetBool("IsMove", isMove);
    }

    /// <summary>AA���q�b�g�����Ƃ��̃A�j���[�V�����C�x���g</summary>
    public void OnAAHit()
    {
        _champAA.AA();
    }
}
