using System;
using Desdiene.Types.AtomicReference;

namespace Desdiene.Types.RangeType
{
    public class Range<T> where T: IComparable<T>
    {
        private readonly Ref<T> minRef = new Ref<T>();
        private readonly Ref<T> maxRef = new Ref<T>();
        public Range(T min, T max)
        {
            SetMin(min);
            SetMax(max);
        }

        public event Action OnMinChanged
        {
            add { minRef.OnValueChanged += value; }
            remove { minRef.OnValueChanged -= value; }
        }

        public event Action OnMaxChanged
        {
            add { maxRef.OnValueChanged += value; }
            remove { maxRef.OnValueChanged -= value; }
        }

        public T Min => minRef.Get();
        public T Max => maxRef.Get();

        public void SetMin(T min) => minRef.Set(min);
        public void SetMax(T max) => maxRef.Set(max);

        //todo: implement me. See IComparable.
        //public bool IsInRange(T value)
        //{
        // 1. Min может быть больше Max. Поэтому сначала определить реальное min и max.
        // 2. Находится ли число в диапазоне?
        //}
    }
}
