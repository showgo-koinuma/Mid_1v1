

public class MinionManager : CharacterManagerBase
{
    MinionState _minionState = MinionState.Moving; // monve managerに必要かも

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {
        if (CharaParam.CurrentHP <= 0) Cmanager.GetCS();  // ラストヒット
    }

    protected override void DeadCharacter()
    {
    }
}