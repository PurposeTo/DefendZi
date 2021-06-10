public class Damage : IDamageDealer
{
    private readonly uint _damageData;

    public Damage() : this(1) { }

    public Damage(uint damageData)
    {
        _damageData = damageData;
    }

    uint IDamageDealer.Value => _damageData;
}
