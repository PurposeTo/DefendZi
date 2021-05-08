using UnityEngine;

public class IntStatPercentable : IntStatClamp, IStat<int>, IPercentStat
{
    private readonly PercentStat percentStat = new PercentStat();

    public IntStatPercentable(int value, int minValue, int maxValue) : base(value, minValue, maxValue) 
    {
        OnStatChange += UpdatePercentValue;
    }

    public float GetPercent()
    {
        return percentStat.Value;
    }

    public static IntStatPercentable operator -(IntStatPercentable stat, int delta)
    {
        stat.Set(stat.Value - delta);
        return stat;
    }

    public static IntStatPercentable operator +(IntStatPercentable stat, int delta)
    {
        stat.Set(stat.Value + delta);
        return stat;
    }

    private void UpdatePercentValue()
    {
        percentStat.Set(Mathf.InverseLerp(minValue, maxValue, Value));
    }
}
