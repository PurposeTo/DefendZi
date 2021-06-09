using System;

public class PlayerScore : IScore
{
    int IScoreGetter.Value => value;
    private int value;

    public event Action OnScoreChanged;

    void IScoreCollector.Add(int amount)
    {
        value += amount;
        OnScoreChanged?.Invoke();
    }
}
