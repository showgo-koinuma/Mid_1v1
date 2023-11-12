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

    /// <summary>csをとったとき/summary> // 引数にgoldが必要かも
    public void GetCS()
    {
        if (this.CharaParam.SideIsBlue) InGameManager.Instance.CsBlue++;
        //else ; redのとき
        Debug.Log("get cs");
    }

    /// <summary>expを取得</summary>
    /// <param name="expValue"></param>
    public void GetExp(int expValue)
    {
        // 範囲にするか絶対取得にするか
    }

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {
        // タワーのターゲットとか？
    }

    protected override void DeadCharacter()
    {
    }
}