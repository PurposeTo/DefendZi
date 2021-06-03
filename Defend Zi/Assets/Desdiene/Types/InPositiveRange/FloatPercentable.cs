using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange.Interfaces;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.InPositiveRange
{
    public class FloatPercentable : FloatInRange, IRef<float>, IReadPercentable
    {
        public FloatPercentable(float value, FloatRange range) : base(value, range) { }

        public float GetPercent()
        {
            return Mathf.InverseLerp(range.Min, range.Max, Get());
        }

        public void SetByPercent(float percent)
        {
            float value = Mathf.Lerp(range.Min, range.Max, percent);
            Set(value);
        }

        //не делать метод SetByPercentAndGet, так как не ясно, что надо вернуть - value или percent.

        public static FloatPercentable operator -(FloatPercentable value, float delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static FloatPercentable operator +(FloatPercentable value, float delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }
    }
}
