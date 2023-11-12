using UnityEngine;

public class ChampionManager : CharacterManagerBase
{
    ChampionAnimationCntlr _champAnimContlr;
    public ChampionAnimationCntlr ChampAnimContlr
    {
        get
        {
            if (_champAnimContlr == null) return _champAnimContlr = GetComponent<ChampionAnimationCntlr>();
            else return _champAnimContlr;
        }
    }

    /// <summary>cs���Ƃ����Ƃ�/summary> // ������gold���K�v����
    public void GetCS()
    {
        if (this.CharaParam.SideIsBlue) InGameManager.Instance.CsBlue++;
        //else ; red�̂Ƃ�
        Debug.Log("get cs");
    }

    /// <summary>exp���擾</summary>
    /// <param name="expValue"></param>
    public void GetExp(int expValue)
    {
        // �͈͂ɂ��邩��Ύ擾�ɂ��邩
    }

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {
        // �^���[�̃^�[�Q�b�g�Ƃ��H
    }

    protected override void DeadCharacter()
    {
    }
}