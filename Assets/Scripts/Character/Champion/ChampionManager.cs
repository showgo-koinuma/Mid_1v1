

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

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {

    }

    protected override void DeadCharacter()
    {
    }
}