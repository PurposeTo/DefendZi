using System;
using UnityEngine;

public class FloatStatClamp : IStat<float>
{
    private protected readonly float minValue;
    private protected readonly float maxValue;

    public event Action OnStatChange;

    public FloatStatClamp(float value, float minValue, float maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Set(value);
    }

    public float Value { get; private set; }

    public virtual void Set(float value)
    {
        this.Value = Mathf.Clamp(value, minValue, maxValue);
        OnStatChange?.Invoke();
    }

    public float SetAndGet(float value)
    {
        Set(value);
        return Value;
    }

    public bool IsMin() => Mathf.Approximately(Value, minValue);

    public bool IsMax() => Mathf.Approximately(Value, maxValue);

    public static FloatStatClamp operator -(FloatStatClamp stat, float delta)
    {
        stat.Set(stat.Value - delta);
        return stat;
    }

    public static FloatStatClamp operator +(FloatStatClamp stat, float delta)
    {
        stat.Set(stat.Value + delta);
        return stat;
    }

    public static implicit operator float(FloatStatClamp stat)
    {
        return stat.Value;
    }
}
