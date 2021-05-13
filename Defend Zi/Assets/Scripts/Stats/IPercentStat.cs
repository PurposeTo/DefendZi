using System;

public interface IPercentStat
{
    event Action OnValueChanged;
    float GetPercent();
}

