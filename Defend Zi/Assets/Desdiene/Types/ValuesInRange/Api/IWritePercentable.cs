﻿namespace Desdiene.Types.ValuesInRange.Api
{
    /// <summary>
    /// Сущность, которой можно записать процентное значние.
    /// </summary>
    public interface IWritePercentable
    {
        void SetByPercent(float percent);
        float SetByPercentAndGet(float percent);
    }
}
