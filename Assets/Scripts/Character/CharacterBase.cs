using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterParameter _charaParam;
    /// <summary>Awake�̃^�C�~���O�Ŏ��s����������������</summary>
    public virtual void AwakeFunction() { }
    /// <summary>yasuoP�p</summary>
    protected event Action OnTakeDamage = null;

    private void Awake()
    {
        Initialization();
        AwakeFunction();
    }

    /// <summary>�p�����[�^������</summary>
    void Initialization()
    {
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
        OnTakeDamage?.Invoke();
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(DamageCalculation.Damage(damage, damageType, _charaParam) + "�_���[�W�󂯂�");
    }

    public Vector2 This2DPos()
    {
        return new Vector2(this.transform.position.x, this.transform.position.z);
    }

    /// <summary>chara�����񂾂Ƃ�</summary>
    protected abstract void DeadCharacter();
}
