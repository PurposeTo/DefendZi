using System;

public class PlayerScore : IScore
{
    private int _value;

    public event Action<int> OnReceived;

    void IScoreCollector.Add(int amount)
    {
        _value += amount;
        OnReceived?.Invoke(amount);
    }

    int IScoreAccessor.Value => _value;
}
