using System;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange.Abstract;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.InPositiveRange
{
    [Serializable]
    public class IntInRange : ValueInRange<int>, IRef<int>
    {
        public IntInRange(int value, IntRange range) : base(value, range) { }

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
