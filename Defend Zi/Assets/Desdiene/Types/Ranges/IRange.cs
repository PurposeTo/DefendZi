using System;

namespace Desdiene.Types.Ranges
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T Length { get; }

        bool IsInRange(T value);

        T Clamp(T value);
    }
}
