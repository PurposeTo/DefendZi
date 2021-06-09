public class Obstacle : IDamageDealer, IScoreGetter
{
    private readonly IDamageDealer damage = new ObstacleDamage();
    private readonly IScoreGetter scorePoints;

    public Obstacle(int scoreByAvoding)
    {
        scorePoints = new ScorePoints(scoreByAvoding);
    }

    int IScoreGetter.Value => scorePoints.Value;

    uint IDamageDealer.Get() => damage.Get();
}
