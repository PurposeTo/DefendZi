using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType.Abstract;

namespace Desdiene.Types.ValuesInRange.Abstract
{
    public abstract class ValueInRange<T> : IRef<T> where T : struct, IComparable<T>
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
            value = ClampValue(value, range.From, range.To);
            valueRef.Set(value);
        }

        /// <summary>
        /// Зажать value между From и To
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        protected abstract T ClampValue(T value, T from, T to);
    }
}
