using System;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.InPositiveRange.Base;
using Desdiene.Types.Ranges.Positive;

namespace Desdiene.Types.InPositiveRange
{
    [Serializable]
    public class IntInRange : ValueInRange<int>, IRef<int>
    {
        public IntInRange(int value, IntRange range) : base(value, range) { }

        protected override bool IsMin => Value == range.Min;
        protected override bool IsMax => Value == range.Max;

        public static IntInRange operator -(IntInRange value, int delta)
        {
            value.Set(value.Value - delta);
            return value;
        }

        public static IntInRange operator -(IntInRange value, uint delta)
        {
            value.Set((int)(value.Value - delta));
            return value;
        }

        public static IntInRange operator +(IntInRange value, int delta)
        {
            value.Set(value.Value + delta);
            return value;
        }

        public static IntInRange operator +(IntInRange value, uint delta)
        {
            value.Set((int)(value.Value + delta));
            return value;
        }

        public static implicit operator int(IntInRange value)
        {
            return value.Value;
        }
    }
}
