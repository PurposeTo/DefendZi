using System;
using Desdiene.Extensions.System.Math;
using UnityEngine;

namespace Desdiene.Types.Ranges.Positive
{
    /// <summary>
    /// Диапазон float от Min до Max.
    /// Не изменяемый.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    [Serializable]
    public struct FloatRange : IRange<float>
    {
        public float Min => _min;
        public float Max => _max;

        //Не делать readonly, тк могут редактироваться через инспектор.
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public FloatRange(float min, float max)
        {
            if (min.CompareTo(max) > 0) throw new FormatException($"Min {min} не может быть больше Max {max}!");

            _min = min;
            _max = max;
        }

        public float Length => Mathf.Abs(Max - Min);

        public float Clamp(float value) => Mathf.Clamp(value, Min, Max);

        public bool IsInRange(float value) => value.Between(Min, Max);
    }
}
