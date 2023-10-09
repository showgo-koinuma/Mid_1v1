using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Param/CharacterParameter")]
/// <summary>キャラクターのパラメータ</summary>
public abstract class CharacterParameter : ScriptableObject
{
    [SerializeField] int _maxHp;
    [SerializeField] float _ad;
    [SerializeField] float _ap;
    [SerializeField] float _ar;
    [SerializeField] float _mr;
    [SerializeField] float _as;
    [SerializeField] int _ms;
    [SerializeField] int _range;
    float _hp;

    public int MaxHP { get => _maxHp; set => _maxHp = value; }
    public float CurrentHP
    {
        get => _hp;
        set
        {
            _hp = value;
            if (_hp > _maxHp) _hp = _maxHp;
            if (_hp <= 0) DeadAction?.Invoke();
        }
    }
    public float AD { get => _ad; set => _ad = value; }
    public float AP { get => _ap; set => _ap = value; }
    public float AR { get => _ar; set => _ar = value; }
    public float MR { get => _mr; set => _mr = value; }
    public float AS { get => _as; set => _as = value; }
    public int MS { get => _ms; set => _ms = value; }
    public int Range { get => _range; }

    /// <summary>Dead時に呼ばれる</summary>
    public event Action DeadAction = default;

    /// <summary>パラメータコンストラクタ</summary>
    /// <param name="data"></param>
    //public CharacterParameter(CharaParamData data)
    //{
    //    _maxHp = data.HP;
    //    _hp = data.HP;
    //    _ad = data.AD;
    //    _ap = data.AP;
    //    _ar = data.AR;
    //    _mr = data.MR;
    //    _as = data.AS;
    //    _ms = data.MS;
    //    _range = data.Range;
    //}
}

[Serializable]
public struct CharaParamData
{
    public int HP, AD, AP, AR, MR, MS, Range;
    public float AS;
}