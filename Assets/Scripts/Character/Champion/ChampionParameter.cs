using System;
using UnityEngine;

/// <summary>チャンピオンのパラメータ</summary>
[CreateAssetMenu(menuName = "Param/ChampionParameter")]
public class ChampionParameter : CharacterParameter
{
    [SerializeField] float _regen;
    [SerializeField] int _hpPerLv;
    [SerializeField] float _adPerLv;
    [SerializeField] float _apPerLv;
    [SerializeField] float _arPerLv;
    [SerializeField] float _mrPerLv;
    [SerializeField] float _asPerLv;
    [SerializeField] float _regenPerLv;
    int _level = 1;
    int _exp = 0;
    int _cdr = 0;
    int _criticalPer = 0;
    int _passiveGauge = 0;

    public float Regen { get => _regen; set => _regen = value; }
    public int Level 
    { 
        get => _level;
        set
        {
            _level = value;
            if (_level > 18) _level = 18;
            else StatusUpdateOnLvUp();
        }
    }
    public int Exp { get => _exp; set => _exp = value; }
    public int Cdr { get => _cdr; set => _cdr = value; }
    public int criticalPer { get => _criticalPer; set => _criticalPer = value; }
    public int PassiveGauge 
    { 
        get => _passiveGauge; 
        set
        {
            _passiveGauge = value;
            if (_passiveGauge >= 100) { _passiveGauge = 100; }
        }
    }
    /// <summary>ステータスに更新があったとき</summary> Lvupとアイテム更新のときかな
    public event Action _onStatusUpdateAction;

    /// <summary>レベルアップによるステータス更新</summary>
    void StatusUpdateOnLvUp()
    {
        MaxHP += _hpPerLv;
        CurrentHP += _hpPerLv;
        AD += _adPerLv;
        AP += _apPerLv;
        AR += _arPerLv;
        MR += _mrPerLv;
        AS += _asPerLv;
        _regen += _regenPerLv;
    }
}
