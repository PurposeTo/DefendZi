using System;

public class PlayerScore : IScore
{
    int IScoreGetter.Value => _value;
    private int _value;

    public event Action OnScoreChanged;

    void IScoreCollector.Add(int amount)
    {
        _value += amount;
        OnScoreChanged?.Invoke();
    }
}
