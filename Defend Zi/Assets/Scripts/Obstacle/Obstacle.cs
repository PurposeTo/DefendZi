public class Obstacle : IDamageDealer, IScore
{
    private readonly IDamageDealer damage = new ObstacleDamage();
    private readonly IScore scorePoints = new ScorePoints();

    int IScore.Value => scorePoints.Value;

    uint IDamageDealer.Get() => damage.Get();
}
