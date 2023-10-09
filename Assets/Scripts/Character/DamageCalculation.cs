 /// <summary>�_���[�W�v�Z</summary>
public static class DamageCalculation
{
    /// <summary>�_���[�W�v�Z�@�h��ђʂ͌��݂Ȃ�</summary>
    /// <param name="damage"></param><param name="damageType"></param><param name="takeDMParam"></param>
    public static int Damage(int damage, DamageType damageType, CharacterParameter takeDMParam)
    {
        switch (damageType)
        {
            case DamageType.AD : return (int)(damage * 100 / (100 + takeDMParam.AR));
            case DamageType.AP : return (int)(damage * 100 / (100 + takeDMParam.MR));
            default : return damage;
        }
    }
}

public enum DamageType
{
    AD,
    AP,
    True
}