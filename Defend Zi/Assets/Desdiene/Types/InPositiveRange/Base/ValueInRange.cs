using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.InPositiveRange.Base
{
    public abstract class ValueInRange<T> : IRef<T> where T : struct, IComparable<T>
    {
        protected readonly IRange<T> range;
        private readonly Ref<T> valueRef;
        public ValueInRange(T value, IRange<T> range)
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

        public abstract bool IsMin { get; }
        public abstract bool IsMax { get; }

        public T Value => valueRef.Get();

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
