using UnityEngine;

public class IntStatPercentable
{
    private readonly int minValue;
    private readonly int maxValue;

    public IntStatPercentable(int value, int minValue, int maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.Value = Mathf.Clamp(value, minValue, maxValue);
    }

    public int Value { get; private set; }

    public void Set(int value)
    {
        this.Value = Mathf.Clamp(value, minValue, maxValue);
    }

    public float GetPercent()
    {
        return (Value - minValue) / (float)maxValue - minValue;
    }

    public bool IsMin() => Value == minValue;

    public bool IsMax() => Value == maxValue;
}
