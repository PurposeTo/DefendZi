using System;
using Desdiene.Extensions.System.Numeric;
using Desdiene.Types.Range.Positive.Interfaces;

namespace Desdiene.Types.Range.Positive.Abstract
{
    /// <summary>
    /// Диапазон от Min до Max.
    /// Не изменяемый класс.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    /// <typeparam name="T">Тип значений диапазона</typeparam>
    public abstract class Range<T> : IRange<T> where T : struct, IComparable<T>
    {
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0) throw new FormatException($"Min {min} не может быть больше Max {max}!");

            Min = min;
            Max = max;
        }

        public abstract T Length { get; }

        public bool IsInRange(T value)
        {
            return value.Between(Min, Max);
        }

        public abstract T Clamp(T value);
    }
}
