using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //[SerializeField] protected CharaParamData _paramData;
    [SerializeField] protected CharacterParameter _charaParam;
    /// <summary>yasuoP�p</summary>
    protected event Action OnTakeDamage;

    private void Awake()
    {
        Initialization();
    }

    /// <summary>�p�����[�^������</summary>
    void Initialization()
    {
        //_charaParam = new CharacterParameter(_paramData);
        _charaParam.DeadAction += DeadCharacter;
    }

    protected abstract void AA(CharacterBase target);

    /// <summary>�_���[�W��^����</summary>
    /// <param name="damage"></param><param name="takenDMChara"></param>
    protected void DealDamage(int damage, DamageType damageType, CharacterBase target)
    {
        target.TakeDamage(damage, damageType);
    }

    /// <summary>�_���[�W���󂯂�</summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage, DamageType damageType)
    {
        OnTakeDamage();
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(DamageCalculation.Damage(damage, damageType, _charaParam) + "�_���[�W�󂯂�");
    }

    /// <summary>chara�����񂾂Ƃ�</summary>
    protected abstract void DeadCharacter();
}