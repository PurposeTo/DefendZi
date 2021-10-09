public class ScorePoints : IScoreAccessor
{
    private readonly uint _amount;

    public ScorePoints() : this(1) { }

    public ScorePoints(uint amount)
    {
        _amount = amount;
    }

    uint IScoreAccessor.Value => _amount;
}
