using UnityEngine;

/// <summary>チャンピオンのアニメーション管理</summary>
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

    /// <summary>AAがヒットしたときのアニメーションイベント</summary>
    public void OnAAHit()
    {
        _champAA.AA();
    }
}
