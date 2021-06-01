using System;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange.Abstract;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    [Serializable]
    public class IntInRange : ValueInRange<int>, IRef<int>
    {
        public IntInRange(int value, IntRange range) : base(value, range) { }

        protected override int ClampValue(int value, int from, int to)
        {
            int min = from;
            int max = to;
            Math.Compare(ref min, ref max);
            return Mathf.Clamp(value, from, max);
        }

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
