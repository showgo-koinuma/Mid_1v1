using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] CharacterParameter _charaParam;
    [SerializeField, Tooltip("�����ڂ�����������KnockUp�p")] protected Transform _body;
    public CharacterParameter CharaParam { get => _charaParam; }
    protected CharacterBase _designatedObject;
    public CharacterBase DesignatedObject { get => _designatedObject;  set => _designatedObject = value; }
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

    /// <summary>�_���[�W��^����</summary>
    /// <param name="damage"></param><param name="takenDMChara"></param>
    public void DealDamage(int damage, DamageType damageType, CharacterBase target)
    {
        target?.TakeDamage(damage, damageType);
    }

    /// <summary>�_���[�W���󂯂�</summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage, DamageType damageType)
    {
        OnTakeDamage?.Invoke();
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(this.gameObject.name + "��" + DamageCalculation.Damage(damage, damageType, _charaParam) + "��" + damageType.ToString() + "�_���[�W");
    }

    public Vector2 This2DPos()
    {
        return new Vector2(this.transform.position.x, this.transform.position.z);
    }

    /// <summary>�L�������m�b�N�A�b�v������(�b)</summary>
    /// <param name="sec"></param>
    public abstract IEnumerator KnockUp(float sec);

    /// <summary>chara�����񂾂Ƃ�</summary>
    protected abstract void DeadCharacter();
}