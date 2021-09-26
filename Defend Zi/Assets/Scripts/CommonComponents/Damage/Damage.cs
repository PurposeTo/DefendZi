public class Damage : IDamage
{
    private readonly uint _damageData;

    public Damage() : this(1) { }

    public Damage(uint damageData)
    {
        _damageData = damageData;
    }

    uint IDamage.Value => _damageData;
}
