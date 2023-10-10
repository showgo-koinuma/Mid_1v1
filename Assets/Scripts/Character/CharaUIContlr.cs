using UnityEngine;
using UnityEngine.UI;

public class CharaUIContlr : MonoBehaviour
{
    [SerializeField] Slider _hpGauge;
    CharacterParameter _charaParam;

    private void Awake()
    {
        _charaParam = GetComponent<CharacterBase>().CharaParam;
        Initialization();
    }

    void Initialization()
    {
        _charaParam.HpUpdate += HpUpdate;
        _charaParam.CurrentHP = _charaParam.MaxHP;
    }

    /// <summary>hp���X�V���ꂽ����UI����</summary>
    void HpUpdate()
    {
        _hpGauge.maxValue = _charaParam.MaxHP;
        _hpGauge.value = _charaParam.CurrentHP;
    }
}
