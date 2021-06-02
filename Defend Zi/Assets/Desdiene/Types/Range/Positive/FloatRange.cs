using Desdiene.Types.Range.Positive.Abstract;
using UnityEngine;

namespace Desdiene.Types.Range.Positive
{
    /// <summary>
    /// Диапазон float от Min до Max.
    /// Не изменяемый класс.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    public class FloatRange : Range<float>
    {
        public FloatRange(float min, float max) : base(min, max) { }

        public override float Length => Mathf.Abs(Max - Min);

        public override float Clamp(float value) => Mathf.Clamp(value, Min, Max);
    }
}
