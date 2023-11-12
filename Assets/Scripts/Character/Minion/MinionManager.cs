

public class MinionManager : CharacterManagerBase
{
    MinionState _minionState = MinionState.Moving;

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {
        if (CharaParam.CurrentHP <= 0) ; 
    }

    protected override void DeadCharacter()
    {
    }
}