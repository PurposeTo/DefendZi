using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange.Interfaces;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    public class FloatPercentable : FloatInRange, IRef<float>, IReadPercentable
    {
        public FloatPercentable(float value, FloatRange range) : base(value, range) { }

        public float GetPercent()
        {
            return Mathf.InverseLerp(range.From, range.To, Get());
        }

        public void SetByPercent(float percent)
        {
            float value = Mathf.Lerp(range.From, range.To, percent);
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
