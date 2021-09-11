using System;

namespace Desdiene.Types.Ranges.Positive
{
    public interface IRange<T> : Ranges.IRange<T> where T : struct, IComparable<T>
    {
        T Min { get; }
        T Max { get; }
    }
}
