using Desdiene.Extensions.System.Math;
using UnityEngine;

namespace Desdiene.Types.Ranges.NonDirectional.Abstract
{
    /// <summary>
    /// Диапазон от "From" до "To".
    /// Не изменяемый.
    /// Нет ограничений на направление диапазона: одно значение может быть как больше другого, так и меньше.
    /// </summary>
    public struct IntRange : IRange<int>
    {
        public int From { get; }
        public int To { get; }

        public IntRange(int from, int to)
        {
            From = from;
            To = to;
        }

        public int Length => Mathf.Abs(From - To);

        public bool IsInRange(int value) => value.Between(From, To);

        public int Clamp(int value)
        {
            int min = From;
            int max = To;
            Math.Compare(ref min, ref max);
            return Mathf.Clamp(value, min, max);
        }
    }
}
