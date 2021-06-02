using Desdiene.Types.Range.Positive.Abstract;
using UnityEngine;

namespace Desdiene.Types.Range.Positive
{
    /// <summary>
    /// Диапазон int от Min до Max.
    /// Не изменяемый класс.
    /// Гарантируется, что Min всегда будет меньше Max.
    /// </summary>
    public class IntRange : Range<int>
    {
        public IntRange(int min, int max) : base(min, max) { }

        public override int Length => Mathf.Abs(Max - Min);

        public override int Clamp(int value) => Mathf.Clamp(value, Min, Max);
    }
}
