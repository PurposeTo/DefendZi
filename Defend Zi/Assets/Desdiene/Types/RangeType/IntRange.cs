using Desdiene.Types.RangeType.Abstract;
using UnityEngine;

namespace Desdiene.Types.RangeType
{
    /// <summary>
    /// Диапазон int от "From" до "To".
    /// Не изменяемый класс.
    /// Нет ограничений на направление диапазона: одно значение может быть как больше другого, так и меньше.
    /// </summary>
    public class IntRange : Range<int>
    {
        public IntRange(int from, int to) : base(from, to) { }

        public override int Length => Mathf.Abs(From - To);
    }
}
