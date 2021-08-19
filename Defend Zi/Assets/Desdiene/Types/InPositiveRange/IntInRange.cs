using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.InPositiveRange.Base;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.InPositiveRange
{
    [Serializable]
    public class IntInRange : ValueInRange<int>, IRef<int>
    {
        public IntInRange(int value, IntRange range) : base(value, range) { }

        public override bool IsMin => Value == range.Min;
        public override bool IsMax => Value == range.Max;

        public static IntInRange operator -(IntInRange value, int delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static IntInRange operator -(IntInRange value, uint delta)
        {
            value.Set((int)(value.Get() - delta));
            return value;
        }

        public static IntInRange operator +(IntInRange value, int delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }

        public static IntInRange operator +(IntInRange value, uint delta)
        {
            value.Set((int)(value.Get() + delta));
            return value;
        }

        public static implicit operator int(IntInRange value)
        {
            return value.Get();
        }
    }
}
