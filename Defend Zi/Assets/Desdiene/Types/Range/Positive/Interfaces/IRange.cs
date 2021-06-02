using System;

namespace Desdiene.Types.Range.Positive.Interfaces
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T Min { get; }
        T Max { get; }
        T Length { get; }

        bool IsInRange(T value);

        T Clamp(T value);
    }
}
