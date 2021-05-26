using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.AtomicReference.Api;
using Desdiene.Types.RangeType;
using UnityEngine;

namespace Desdiene.Types.ValuesInRange
{
    public class FloatInRange : IRef<float>
    {
        protected readonly Range<float> range;
        private readonly Ref<float> valueRef;
        public FloatInRange(float value, Range<float> range)
        {
            this.range = range;
            valueRef = new Ref<float>(value);
            Set(value);
        }

        public event Action OnValueChanged
        {
            add { valueRef.OnValueChanged += value; }
            remove { valueRef.OnValueChanged -= value; }
        }

        public bool IsMin() => Mathf.Approximately(Get(), range.Min);

        public bool IsMax() => Mathf.Approximately(Get(), range.Max);

        public float SetAndGet(float value)
        {
            Set(value);
            return Get();
        }

        public float Get() => valueRef.Get();

        public void Set(float value)
        {
            value = Mathf.Clamp(value, range.Min, range.Max);
            valueRef.Set(value);
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
