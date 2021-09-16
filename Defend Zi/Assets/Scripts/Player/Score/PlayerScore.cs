using System;

public class PlayerScore : IScore
{
    private int _value;

    public event Action OnReceived;

    void IScoreCollector.Add(int amount)
    {
        _value += amount;
        OnReceived?.Invoke();
    }

    int IScoreAccessor.Value => _value;
}
