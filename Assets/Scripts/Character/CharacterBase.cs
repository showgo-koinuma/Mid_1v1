using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] private CharacterParameter _charaParam;
    public CharacterParameter CharaParam { get => _charaParam; }
    protected CharacterBase _designatedObject;
    public CharacterBase DesignatedObject { get => _designatedObject;  set => _designatedObject = value; }
    /// <summary>Awakeのタイミングで実行したい処理を書く</summary>
    public virtual void AwakeFunction() { }
    /// <summary>yasuoP用</summary>
    protected event Action OnTakeDamage = null;

    private void Awake()
    {
        Initialization();
        AwakeFunction();
    }

    /// <summary>パラメータ初期化</summary>
    void Initialization()
    {
        _charaParam.DeadAction += DeadCharacter;
    }

    /// <summary>ダメージを与える</summary>
    /// <param name="damage"></param><param name="takenDMChara"></param>
    public void DealDamage(int damage, DamageType damageType, CharacterBase target)
    {
        target?.TakeDamage(damage, damageType);
    }

    /// <summary>ダメージを受ける</summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage, DamageType damageType)
    {
        OnTakeDamage?.Invoke();
        _charaParam.CurrentHP -= DamageCalculation.Damage(damage, damageType, _charaParam);
        Debug.Log(DamageCalculation.Damage(damage, damageType, _charaParam) + "の" + damageType.ToString() + "ダメージ");
    }

    public Vector2 This2DPos()
    {
        return new Vector2(this.transform.position.x, this.transform.position.z);
    }

    /// <summary>charaが死んだとき</summary>
    protected abstract void DeadCharacter();
}
