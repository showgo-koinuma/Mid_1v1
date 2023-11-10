using UnityEngine;

/// <summary>チャンピオンのアニメーション管理</summary>
public class ChampionAnimationCntlr : MonoBehaviour
{
    Animator _animator;
    ChampionManager _champManager;
    ChampAA _champAA;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _champManager = GetComponent<ChampionManager>();
        _champAA = _champManager.gameObject.GetComponent<ChampAA>();
    }

    public void SetIsMove(bool isMove) => _animator.SetBool("IsMove", isMove);
    public void SetSpeed(float speed) => _animator.SetFloat("Speed", speed);
    public void AATrigger() => _animator.SetTrigger("AA");
    public void StartQAnimation() => _animator.SetTrigger("Q");
    public void StartQ3Animation() => _animator.SetTrigger("Q3");


    /// <summary>AAがヒットしたときのアニメーションイベント</summary>
    public void OnAAHit()
    {
        _champAA.AA();
    }
}
