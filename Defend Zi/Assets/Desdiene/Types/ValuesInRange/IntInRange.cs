using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    [Serializable]
    public class IntInRange : IRef<int>
    {
        protected readonly Range<int> range;
        private readonly Ref<int> valueRef;
        public IntInRange(int value, Range<int> range)
        {
            this.range = range;
            valueRef = new Ref<int>(value);
            Set(value);
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnValueChanged += value;
            remove => valueRef.OnValueChanged -= value;
        }

        public bool IsMin() => Get() == range.Min;

        public bool IsMax() => Get() == range.Max;

        public int SetAndGet(int value)
        {
            Set(value);
            return Get();
        }

        public int Get() => valueRef.Get();

        public void Set(int value)
        {
            value = Mathf.Clamp(value, range.Min, range.Max);
            valueRef.Set(value);
        }

        public static IntInRange operator -(IntInRange value, int delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static IntInRange operator +(IntInRange value, int delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }

        public static implicit operator int(IntInRange value)
        {
            return value.Get();
        }
    }
}
