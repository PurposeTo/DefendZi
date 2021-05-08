using System;

public interface IPercentStat
{
    event Action OnStatChange;
    float GetPercent();
}

