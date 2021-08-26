using System;
using Desdiene.Extensions.System.Math;
using UnityEngine;

namespace Desdiene.Types.Ranges.Positive
{
    /// <summary>
    /// Диапазон int от Min до Max.
    /// Не изменяемый.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    [Serializable]
    public struct IntRange : IRange<int>
    {
        public int Min => _min;
        public int Max => _max;

        //Не делать readonly, тк могут редактироваться через инспектор.
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public IntRange(int min, int max)
        {
            if (min.CompareTo(max) > 0) throw new FormatException($"Min {min} не может быть больше Max {max}!");

            _min = min;
            _max = max;
        }

        public int Length => Mathf.Abs(Max - Min);

        public int Clamp(int value) => Mathf.Clamp(value, Min, Max);

        public bool IsInRange(int value) => value.Between(Min, Max);
    }
}
