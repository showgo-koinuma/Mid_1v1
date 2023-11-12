using System;
using UnityEngine;

public abstract class CharacterManagerBase : MonoBehaviour
{
    [SerializeField] CharacterParameter _charaParam;
    public CharacterParameter CharaParam { get => _charaParam; }
    CharacterMoveManager _moveManager;
    public CharacterMoveManager MoveManager { get => _moveManager; }
    /// <summary>yasuoP用</summary>
    protected event Action OnTakeDamage = null;

    private void Awake()
    {
        Initialization();
        _moveManager = GetComponent<CharacterMoveManager>();
    }

    /// <summary>パラメータ初期化</summary>
    void Initialization()
    {
        _charaParam.DeadAction += DeadCharacter;
    }

    /// <summary>ダメージを与える</summary>
    /// <param name="damage"></param><param name="takenDMChara"></param>
    public void DealDamage(int damage, DamageType damageType, CharacterManagerBase target)
    {
        target?.TakeDamage(damage, damageType, this);
    }

    /// <summary>ダメージを受ける</summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage, DamageType damageType, CharacterManagerBase dealer)
    {
        OnTakeDamage?.Invoke();
        if (dealer.gameObject.TryGetComponent(out ChampionManager Cmanager)) WhenTakeDamageFormChampion(Cmanager);
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(this.gameObject.name + "に" + DamageCalculation.Damage(damage, damageType, _charaParam) + "の" + damageType.ToString() + "ダメージ");
    }

    /// <summary>HPに更新があったときの処理</summary>
    void OnHPUpdate() // 
    {
    }

    /// <summary>チャンピオンからダメージを受けたときの処理</summary>
    protected abstract void WhenTakeDamageFormChampion(ChampionManager Cmanager);

    /// <summary>charaが死んだとき</summary>
    protected abstract void DeadCharacter();
}