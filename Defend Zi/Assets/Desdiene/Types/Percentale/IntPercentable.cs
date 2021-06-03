using System;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange.Positive;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentable
{
    public class IntPercentable : IntInRange, IRef<int>, IPercent
    {
        public IntPercentable(int value, IntRange range) : base(value, range) { }

        event Action IPercentOnChanged.OnValueChanged
        {
            add => OnValueChanged += value;
            remove => OnValueChanged -= value;
        }

        /// <summary>
        /// Получить процентное значение.
        /// </summary>
        /// <returns></returns>
        float IReadPercent.Get() => GetPercent();

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        void IWritePercent.Set(float percent) => SetByPercent(percent);

        float IWritePercent.SetAndGet(float percent) => SetByPercentAndGet(percent);

        public float GetPercent() => Mathf.InverseLerp(range.Min, range.Max, Get());

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>s
        public void SetByPercent(float percent)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(range.Min, range.Max, percent));
            Set(value);
        }

        public float SetByPercentAndGet(float percent)
        {
            SetByPercent(percent);
            return GetPercent();
        }

        public static IntPercentable operator -(IntPercentable value, int delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static IntPercentable operator +(IntPercentable value, int delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }
    }
}
