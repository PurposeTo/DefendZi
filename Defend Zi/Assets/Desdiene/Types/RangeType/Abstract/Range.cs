using System;
using Desdiene.Extensions.System.Numeric;
using Desdiene.Types.RangeType.Interfaces;

namespace Desdiene.Types.RangeType.Abstract
{
    /// <summary>
    /// Диапазон от "From" до "To".
    /// Не изменяемый класс.
    /// Нет ограничений на направление диапазона: одно значение может быть как больше другого, так и меньше.
    /// </summary>
    /// <typeparam name="T">Тип значений диапазона</typeparam>
    public abstract class Range<T> : IRange<T> where T : struct, IComparable<T>
    {
        public T From { get; }
        public T To { get; }

        public Range(T from, T to)
        {
            From = from;
            To = to;
        }

        public abstract T Length { get; }

        public bool IsInRange(T value)
        {
            return value.Between(From, To);
        }
    }
}
