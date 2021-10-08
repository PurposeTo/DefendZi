using Desdiene.Types.AtomicReferences;
using Desdiene.Types.InPositiveRange.Base;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

namespace Desdiene.Types.InPositiveRange
{
    public class FloatInRange : ValueInRange<float>, IRef<float>
    {
        public FloatInRange(float value, FloatRange range) : base(value, range) { }

        protected override bool IsMin => Mathf.Approximately(Value, range.Min);

        protected override bool IsMax => Mathf.Approximately(Value, range.Max);

        public static FloatInRange operator -(FloatInRange value, float delta)
        {
            value.Set(value.Value - delta);
            return value;
        }

        public static FloatInRange operator +(FloatInRange value, float delta)
        {
            value.Set(value.Value + delta);
            return value;
        }

        public static implicit operator float(FloatInRange value)
        {
            return value.Value;
        }
    }
}
