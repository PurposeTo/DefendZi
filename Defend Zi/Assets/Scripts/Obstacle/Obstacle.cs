public class Obstacle : IDamageDealer, IScoreGetter
{
    private readonly IDamageDealer damage = new Damage();
    private readonly IScoreGetter score;

    public Obstacle(int scoreByAvoding)
    {
        score = new ScoreGetter(scoreByAvoding);
    }

    int IScoreGetter.Value => score.Value;

    uint IDamageDealer.Value => damage.Value;
}
