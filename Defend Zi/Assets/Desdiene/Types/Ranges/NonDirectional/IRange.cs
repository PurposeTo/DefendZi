using System;

namespace Desdiene.Types.Ranges.NonDirectional.Interfaces
{
    public interface IRange<T> : Ranges.IRange<T> where T : struct, IComparable<T>
    {
        T From { get; }
        T To { get; }
    }
}
