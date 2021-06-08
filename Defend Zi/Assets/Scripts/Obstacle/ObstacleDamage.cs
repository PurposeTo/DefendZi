public class ObstacleDamage : IDamageDealer
{
    private readonly uint damage = 1;
    uint IDamageDealer.Get() => damage;
}
