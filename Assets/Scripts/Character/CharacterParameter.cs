using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Param/CharacterParameter")]
/// <summary>キャラクターのパラメータ</summary>
public class CharacterParameter : ScriptableObject
{
    [SerializeField] bool _sideIsBlue;
    [SerializeField] int _maxHp;
    [SerializeField] float _ad;
    [SerializeField] float _ap;
    [SerializeField] float _ar;
    [SerializeField] float _mr;
    [SerializeField] float _as;
    [SerializeField] int _ms;
    [SerializeField] int _range;
    float _hp;

    public bool SideIsBlue { get => _sideIsBlue; }
    public int MaxHP { get => _maxHp; set => _maxHp = value; }
    public float CurrentHP
    {
        get => _hp;
        set
        {
            _hp = value;
            if (_hp > _maxHp) _hp = _maxHp;
            HpUpdate?.Invoke();
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
    public event Action HpUpdate = default;
}