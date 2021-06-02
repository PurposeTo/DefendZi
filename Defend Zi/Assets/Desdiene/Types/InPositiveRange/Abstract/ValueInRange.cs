using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.Range.Positive.Abstract;

namespace Desdiene.Types.InPositiveRange.Abstract
{
    public class ValueInRange<T> : IRef<T> where T : struct, IComparable<T>
    {
        protected readonly Range<T> range;
        private readonly Ref<T> valueRef;
        public ValueInRange(T value, Range<T> range)
        {
            this.range = range;
            valueRef = new Ref<T>(value);
            Set(value);
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnValueChanged += value;
            remove => valueRef.OnValueChanged -= value;
        }

        public T SetAndGet(T value)
        {
            Set(value);
            return Get();
        }

        public T Get() => valueRef.Get();

        public void Set(T value)
        {
            value = range.Clamp(value);
            valueRef.Set(value);
        }
    }
}
