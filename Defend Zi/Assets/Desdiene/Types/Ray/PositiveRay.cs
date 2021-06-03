using System;

namespace Desdiene.Types.Ray
{
    /// <summary>
    /// Луч от заданной точки до плюс бесконечности
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct PositiveRay<T> : IRay<T> where T : struct, IComparable<T>
    {
        public T StartPoint { get; }

        public PositiveRay(T startPoint)
        {
            StartPoint = startPoint;
        }

        public T Clamp(T value) => Math.ClampMin(value, StartPoint);
    }
}
