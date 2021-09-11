using System;

namespace Desdiene.Types.Rays
{
    public interface IRay<T> where T : struct, IComparable<T>
    {
        T StartPoint { get; }
        T Clamp(T value);
    }
}
