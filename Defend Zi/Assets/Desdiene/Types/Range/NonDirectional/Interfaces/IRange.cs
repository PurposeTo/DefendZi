using System;

namespace Desdiene.Types.Range.NonDirectional.Interfaces
{
    public interface IRange<T> where T : struct, IComparable<T>
    {
        T From { get; }
        T To { get; }
        T Length { get; }

        bool IsInRange(T value);

        T Clamp(T value);
    }
}
