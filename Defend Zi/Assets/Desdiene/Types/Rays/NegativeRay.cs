using System;

namespace Desdiene.Types.Rays
{
    /// <summary>
    /// Луч от заданной точки до минус бесконечности
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct NegativeRay<T> : IRay<T> where T : struct, IComparable<T>
    {
        public T StartPoint { get; }

        public NegativeRay(T startPoint)
        {
            StartPoint = startPoint;
        }

        public T Clamp(T value) => Math.ClampMax(value, StartPoint);
    }
}
