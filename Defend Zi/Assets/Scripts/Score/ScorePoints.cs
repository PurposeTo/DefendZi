public class ScorePoints : IScoreGetter
{
    private readonly int points;

    public ScorePoints() : this(1) { }

    public ScorePoints(int points)
    {
        this.points = points;
    }

    int IScoreGetter.Value => points;
}
