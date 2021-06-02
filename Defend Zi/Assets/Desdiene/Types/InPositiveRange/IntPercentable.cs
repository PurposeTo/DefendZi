using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange.Interfaces;
using Desdiene.Types.InPositiveRange.Positive;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.InPositiveRange
{
    public class IntPercentable : IntInRange, IRef<int>, IPercentable
    {
        public IntPercentable(int value, IntRange range) : base(value, range) { }

        public float GetPercent()
        {
            return Mathf.InverseLerp(range.Min, range.Max, Get());
        }

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
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
