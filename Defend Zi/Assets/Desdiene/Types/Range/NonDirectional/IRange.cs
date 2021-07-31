using System;

namespace Desdiene.Types.Range.NonDirectional.Interfaces
{
    public interface IRange<T> : Range.IRange<T> where T : struct, IComparable<T>
    {
        T From { get; }
        T To { get; }
    }
}
