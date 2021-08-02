using System;

namespace Desdiene.Types.Range
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T Length { get; }

        bool IsInRange(T value);

        T Clamp(T value);
    }
}
