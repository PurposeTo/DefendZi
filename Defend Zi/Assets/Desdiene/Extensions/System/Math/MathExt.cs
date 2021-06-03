using System;

namespace Desdiene.Extensions.System.Math
{
    public static class MathExt
    {
        public static bool Between<T>(this T value, T inclusiveMin, T inclusiveMax) where T : IComparable<T>
        {
            return Desdiene.Math.Between(value, inclusiveMin, inclusiveMax);
        }
    }
}
