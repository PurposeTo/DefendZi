public class PlayerScore : IScoreCollector
{
    int IScore.Value => value;
    private int value;

    void IScoreCollector.Add(int amount)
    {
        value += amount;
    }
}
