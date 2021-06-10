public class ScoreGetter : IScoreGetter
{
    private readonly int _scoreData;

    public ScoreGetter() : this(1) { }

    public ScoreGetter(int scoreData)
    {
        _scoreData = scoreData;
    }

    int IScoreGetter.Value => _scoreData;
}
