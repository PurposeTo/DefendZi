using System;

namespace Desdiene.Extensions.System.Numeric
{
    public static class MathExt
    {
        public static bool Between<T>(this T value, T inclusiveMin, T inclusiveMax) where T : IComparable<T>
        {
            return Math.Between(value, inclusiveMin, inclusiveMax);
        }
    }
}
