using System;
using Desdiene.Extensions.System.Math;
using UnityEngine;

namespace Desdiene.Types.Range.Positive
{
    /// <summary>
    /// Диапазон float от Min до Max.
    /// Не изменяемый.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    public struct FloatRange : IRange<float>
    {
        public float Min { get; }
        public float Max { get; }

        public FloatRange(float min, float max)
        {
            if (min.CompareTo(max) > 0) throw new FormatException($"Min {min} не может быть больше Max {max}!");

            Min = min;
            Max = max;
        }

        public float Length => Mathf.Abs(Max - Min);

        public float Clamp(float value) => Mathf.Clamp(value, Min, Max);

        public bool IsInRange(float value) => value.Between(Min, Max);
    }
}
