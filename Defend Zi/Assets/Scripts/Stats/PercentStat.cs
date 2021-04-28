using UnityEngine;

public class PercentStat
{
    public PercentStat() : this(0f) { }

    public PercentStat(float value)
    {
        Set(value);
    }

    public float Value { get; private set; }

    public void Set(float value)
    {
        Value = Mathf.Clamp(value, 0f, 1f);
    }

    public bool IsMin() => Mathf.Approximately(Value, 0);

    public bool IsMax() => Mathf.Approximately(Value, 1);

    public static PercentStat operator -(PercentStat percentStat, float delta)
    {
        percentStat.Set(percentStat.Value - delta);
        return percentStat;
    }

    public static PercentStat operator +(PercentStat percentStat, float delta)
    {
        percentStat.Set(percentStat.Value + delta);
        return percentStat;
    }
}
