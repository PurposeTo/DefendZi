using System;

public class PlayerScore : IScore
{
    int IScoreGetter.Value => _value;
    private int _value;

    public event Action OnChanged;

    void IScoreCollector.Add(int amount)
    {
        _value += amount;
        OnChanged?.Invoke();
    }
}
