

public class MinionManager : CharacterManagerBase
{
    MinionState _minionState = MinionState.Moving; // monve manager�ɕK�v����

    protected override void WhenTakeDamageFormChampion(ChampionManager Cmanager)
    {
        if (CharaParam.CurrentHP <= 0) Cmanager.GetCS();  // ���X�g�q�b�g
    }

    protected override void DeadCharacter()
    {
    }
}