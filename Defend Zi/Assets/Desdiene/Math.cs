using System;
using System.Collections.Generic;
using System.Linq;

namespace Desdiene
{
    public static class Math
    {
        public static void Compare<T>(ref T realMin, ref T realMax)
        {
            List<T> values = new List<T>
            {
                realMin,
                realMax
            };

            realMin = values.Min();
            realMax = values.Max();
        }

        public static bool Between<T>(T value, T minValue, T maxValue) where T : IComparable<T>
        {
            Compare(ref minValue, ref maxValue);
            return value.CompareTo(minValue) >= 0 ||
                value.CompareTo(maxValue) <= 0;
        }
    }
}
