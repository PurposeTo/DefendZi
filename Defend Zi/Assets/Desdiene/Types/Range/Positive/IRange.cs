using System;

namespace Desdiene.Types.Range.Positive
{
    public interface IRange<T> : Interfaces.IRange<T> where T : struct, IComparable<T>
    {
        T Min { get; }
        T Max { get; }
    }
}
