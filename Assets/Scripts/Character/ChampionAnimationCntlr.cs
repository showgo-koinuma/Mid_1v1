using UnityEngine;

/// <summary>�`�����s�I���̃A�j���[�V�����Ǘ�</summary>
public class ChampionAnimationCntlr : MonoBehaviour
{
    Animator _animator;
    ChampionController _champContlr;
    ChampAA _champAA;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _champContlr = transform.parent.gameObject.GetComponent<ChampionController>();
        _champAA = _champContlr.gameObject.GetComponent<ChampAA>();
    }

    public void StartAAAnimation()
    {
        _animator.SetTrigger("AA");
    }

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
