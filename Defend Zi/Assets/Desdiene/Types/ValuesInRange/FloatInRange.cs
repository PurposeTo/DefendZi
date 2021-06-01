using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange.Abstract;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    public class FloatInRange : ValueInRange<float>, IRef<float>
    {
        public FloatInRange(float value, FloatRange range) : base(value, range) { }

        protected override float ClampValue(float value, float from, float to)
        {
            float min = from;
            float max = to;
            Math.Compare(ref min, ref max);
            return Mathf.Clamp(value, from, max);
        }

        public static FloatInRange operator -(FloatInRange value, float delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static FloatInRange operator +(FloatInRange value, float delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }

        public static implicit operator float(FloatInRange value)
        {
            return value.Get();
        }
    }
}
