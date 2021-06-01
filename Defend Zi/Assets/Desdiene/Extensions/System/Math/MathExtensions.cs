using System;

namespace Desdiene.Extensions.System.Math
{
    public static class MathExtensions
    {
        public static bool Between<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return Between(value, minValue, maxValue);
        }
    }
}
