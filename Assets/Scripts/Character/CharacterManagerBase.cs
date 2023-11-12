using System;
using UnityEngine;

public abstract class CharacterManagerBase : MonoBehaviour
{
    [SerializeField] CharacterParameter _charaParam;
    public CharacterParameter CharaParam { get => _charaParam; }
    CharacterMoveManager _moveManager;
    public CharacterMoveManager MoveManager { get => _moveManager; }
    /// <summary>yasuoP�p</summary>
    protected event Action OnTakeDamage = null;

    private void Awake()
    {
        Initialization();
        _moveManager = GetComponent<CharacterMoveManager>();
    }

    /// <summary>�p�����[�^������</summary>
    void Initialization()
    {
        _charaParam.DeadAction += DeadCharacter;
    }

    /// <summary>�_���[�W��^����</summary>
    /// <param name="damage"></param><param name="takenDMChara"></param>
    public void DealDamage(int damage, DamageType damageType, CharacterManagerBase target)
    {
        target?.TakeDamage(damage, damageType, this);
    }

    /// <summary>�_���[�W���󂯂�</summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage, DamageType damageType, CharacterManagerBase dealer)
    {
        OnTakeDamage?.Invoke();
        if (dealer.gameObject.TryGetComponent(out ChampionManager Cmanager)) WhenTakeDamageFormChampion(Cmanager);
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(this.gameObject.name + "��" + DamageCalculation.Damage(damage, damageType, _charaParam) + "��" + damageType.ToString() + "�_���[�W");
    }

    /// <summary>HP�ɍX�V���������Ƃ��̏���</summary>
    void OnHPUpdate() // 
    {
    }

    /// <summary>�`�����s�I������_���[�W���󂯂��Ƃ��̏���</summary>
    protected abstract void WhenTakeDamageFormChampion(ChampionManager Cmanager);

    /// <summary>chara�����񂾂Ƃ�</summary>
    protected abstract void DeadCharacter();
}