using UnityEngine;

public class PercentStat
{
    public PercentStat(float value)
    {
        this.Value = Mathf.Clamp(value, 0f, 1f);
    }

    public float Value { get; private set; }

    public void Set(float value)
    {
        this.Value = Mathf.Clamp(value, 0f, 1f);
    }
}
