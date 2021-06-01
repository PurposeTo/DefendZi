using Desdiene.Types.RangeType.Abstract;
using UnityEngine;

namespace Desdiene.Types.RangeType
{
    /// <summary>
    /// Диапазон float от "From" до "To".
    /// Не изменяемый класс.
    /// Нет ограничений на направление диапазона: одно значение может быть как больше другого, так и меньше.
    /// </summary>
    public class FloatRange : Range<float>
    {
        public FloatRange(float from, float to) : base(from, to) { }

        public override float Length => Mathf.Abs(From - To);
    }
}
