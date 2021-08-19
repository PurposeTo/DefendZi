using Desdiene.Types.AtomicReference;
using Desdiene.Types.InPositiveRange.Base;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.InPositiveRange
{
    public class FloatInRange : ValueInRange<float>, IRef<float>
    {
        public override bool IsMin => Mathf.Approximately(Value, range.Min);

        public override bool IsMax => Mathf.Approximately(Value, range.Max);

        public FloatInRange(float value, FloatRange range) : base(value, range) { }

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
