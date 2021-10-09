using System;

public class PlayerScore : IScore
{
    private uint _value;

    public event Action<uint> OnReceived;

    void IScoreCollector.Add(uint amount)
    {
        _value += amount;
        OnReceived?.Invoke(amount);
    }

    uint IScoreAccessor.Value => _value;
}
