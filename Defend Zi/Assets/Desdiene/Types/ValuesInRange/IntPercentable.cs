using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange.Interfaces;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    public class IntPercentable : IntInRange, IRef<int>, IPercentable
    {
        public IntPercentable(int value, IntRange range) : base(value, range) { }

        public float GetPercent()
        {
            return Mathf.InverseLerp(range.From, range.To, Get());
        }

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        public void SetByPercent(float percent)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(range.From, range.To, percent));
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
