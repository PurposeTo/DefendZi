using System;

namespace Desdiene.Types.RangeType.Interfaces
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T From { get; }
        T To { get; }
        T Length { get; }

        bool IsInRange(T value);
    }
}
