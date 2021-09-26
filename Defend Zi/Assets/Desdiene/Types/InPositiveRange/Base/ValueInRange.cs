using System;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Ranges.Positive;

namespace Desdiene.Types.InPositiveRange.Base
{
    public abstract class ValueInRange<T> : IRef<T> where T : struct, IComparable<T>
    {
        protected readonly IRange<T> range;
        protected readonly IRef<T> valueRef;
        public ValueInRange(T value, IRange<T> range)
        {
            this.range = range;
            valueRef = new Ref<T>(value);
            Set(value);
        }

        event Action IRefNotifier.OnChanged
        {
            add => valueRef.OnChanged += value;
            remove => valueRef.OnChanged -= value;
        }

        T IRefAccessor<T>.Value => Value;

        void IRefMutator<T>.Set(T value) => Set(value);

        T IRef<T>.SetAndGet(T value)
        {
            Set(value);
            return Value;
        }

        protected abstract bool IsMin { get; }
        protected abstract bool IsMax { get; }
        protected T Value => valueRef.Value;

        protected void Set(T value)
        {
            value = range.Clamp(value);
            valueRef.Set(value);
        }
    }
}
