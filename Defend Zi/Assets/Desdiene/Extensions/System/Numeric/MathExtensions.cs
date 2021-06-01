using System;

namespace Desdiene.Extensions.System.Numeric
{
    public static class MathExtensions
    {
        public static bool Between<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return Math.Between(value, minValue, maxValue);
        }
    }
}
