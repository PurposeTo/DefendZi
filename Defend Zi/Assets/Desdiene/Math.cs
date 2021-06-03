using System;
using System.Collections.Generic;
using System.Linq;

namespace Desdiene
{
    public struct Math
    {
        /// <summary>
        /// Сравнить и вернуть минимальное и максимальное значения из двух
        /// </summary>
        /// <typeparam name="T">Тип сравниваемых значений.</typeparam>
        /// <param name="realMin">Вход - первое значение. Выход - минимальное значение.</param>
        /// <param name="realMax">Вход - второе значение. Выход - максимальное значение.</param>
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

        public static bool Between<T>(T value, T inclusiveMin, T inclusiveMax) where T : IComparable<T>
        {
            Compare(ref inclusiveMin, ref inclusiveMax);
            return value.CompareTo(inclusiveMin) >= 0 ||
                value.CompareTo(inclusiveMax) <= 0;
        }

        public static T ClampMin<T>(T value, T min) where T : struct, IComparable<T>
        {
            return value.CompareTo(min) < 0
                ? min
                : value;
        }

        public static T ClampMax<T>(T value, T max) where T : struct, IComparable<T>
        {
            return value.CompareTo(max) > 0
                ? max
                : value;
        }
    }
}
