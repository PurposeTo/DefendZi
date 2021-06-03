using System;
using Desdiene.Extensions.System.Math;
using UnityEngine;

namespace Desdiene.Types.Range.Positive
{
    /// <summary>
    /// Диапазон int от Min до Max.
    /// Не изменяемый.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    public struct IntRange : IRange<int>
    {
        public int Min { get; }
        public int Max { get; }

        public IntRange(int min, int max)
        {
            if (min.CompareTo(max) > 0) throw new FormatException($"Min {min} не может быть больше Max {max}!");

            Min = min;
            Max = max;
        }

        public int Length => Mathf.Abs(Max - Min);

        public int Clamp(int value) => Mathf.Clamp(value, Min, Max);

        public bool IsInRange(int value) => value.Between(Min, Max);
    }
}
