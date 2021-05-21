using System;
using UnityEngine;

public class IntStatClamp: IStat<int>
{
    private protected readonly int minValue;
    private protected readonly int maxValue;

    public event Action OnValueChanged;

    public IntStatClamp(int value, int minValue, int maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Set(value);
    }

    public int Value { get; private set; }

    public virtual void Set(int value)
    {
        if (this.Value != value)
        {
            this.Value = Mathf.Clamp(value, minValue, maxValue);
            OnValueChanged?.Invoke();
        }
    }

    public int SetAndGet(int value)
    {
        Set(value);
        return Value;
    }

    public bool IsMin() => Value == minValue;

    public bool IsMax() => Value == maxValue;

    public static IntStatClamp operator -(IntStatClamp stat, int delta)
    {
        stat.Set(stat.Value - delta);
        return stat;
    }

    public static IntStatClamp operator +(IntStatClamp stat, int delta)
    {
        stat.Set(stat.Value + delta);
        return stat;
    }

    public static implicit operator int(IntStatClamp stat)
    {
        return stat.Value;
    }
}
