using Desdiene.Types.AtomicReference;
using Desdiene.Types.RangeType;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    public class IntPercentable : IntInRange, IRef<int>, IPercentable
    {
        public IntPercentable(int value, Range<int> range) : base(value, range) { }

        public float GetPercent()
        {
            return Mathf.InverseLerp(Min, Max, Get());
        }

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        public void SetByPercent(float percent)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(Min, Max, percent));
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
