using UnityEngine;

public class FloatStatPercentable : FloatStatClamp, IStat<float>
{
    private readonly PercentStat percentStat = new PercentStat();

    public FloatStatPercentable(float value, float minValue, float maxValue) : base(value, minValue, maxValue) 
    {
        OnValueChanged += UpdatePercentValue;
    }

    public FloatStatClamp GetPercent()
    {
        return percentStat;
    }

    public override void Set(float value)
    {
        base.Set(value);
    }

    public void SetByPercent(float percent)
    {
        percentStat.Set(percent);
        Set(Mathf.Lerp(minValue, maxValue, percentStat.Value));
    }

    public float SetByPercentAndGet(float percent)
    {
        SetByPercent(percent);
        return Value;
    }

    public static FloatStatPercentable operator -(FloatStatPercentable stat, int delta)
    {
        stat.Set(stat.Value - delta);
        return stat;
    }

    public static FloatStatPercentable operator +(FloatStatPercentable stat, int delta)
    {
        stat.Set(stat.Value + delta);
        return stat;
    }

    private void UpdatePercentValue()
    {
        percentStat.Set(Mathf.InverseLerp(minValue, maxValue, Value));
    }
}
