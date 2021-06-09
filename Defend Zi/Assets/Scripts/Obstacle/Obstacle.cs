public class Obstacle : IDamageDealer, IScoreGetter
{
    private readonly IDamageDealer damage = new ObstacleDamage();
    private readonly IScoreGetter scorePoints = new ScorePoints();

    int IScoreGetter.Value => scorePoints.Value;

    uint IDamageDealer.Get() => damage.Get();
}
