public class ScorePoints : IScoreAccessor
{
    private readonly int _amount;

    public ScorePoints() : this(1) { }

    public ScorePoints(int amount)
    {
        _amount = amount;
    }

    int IScoreAccessor.Value => _amount;
}
