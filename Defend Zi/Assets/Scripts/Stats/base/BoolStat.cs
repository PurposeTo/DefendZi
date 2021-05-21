using System;

public class BoolStat : IStat<bool>
{
    public event Action OnValueChanged;

    public BoolStat() : this(false) { }

    public BoolStat(bool value)
    {
        Set(value);
    }

    public bool Value { get; private set; }

    public virtual void Set(bool value)
    {
        if (this.Value != value)
        {
            this.Value = value;
            OnValueChanged?.Invoke();
        }
    }

    public bool SetAndGet(bool value)
    {
        Set(value);
        return Value;
    }

    public static implicit operator bool(BoolStat stat)
    {
        return stat.Value;
    }
}
