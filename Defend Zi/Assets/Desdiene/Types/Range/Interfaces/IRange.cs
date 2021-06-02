using System;

namespace Desdiene.Types.Range.Interfaces
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T Length { get; }

        bool IsInRange(T value);

        T Clamp(T value);
    }
}
