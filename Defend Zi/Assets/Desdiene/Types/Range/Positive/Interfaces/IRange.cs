using System;

namespace Desdiene.Types.Range.Positive.Interfaces
{
    public interface IRange<T> : Range.Interfaces.IRange<T> where T : struct, IComparable<T>
    {
        T Min { get; }
        T Max { get; }
    }
}
