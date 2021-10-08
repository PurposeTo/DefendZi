using System;
using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Percentale;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentables
{
    public class IntPercentable : IntInRange, IPercentable<int>
    {
        public IntPercentable(int value, IntRange range) : base(value, range) { }

        event Action IPercentNotifier.OnChanged
        {
            add => valueRef.OnChanged += value;
            remove => valueRef.OnChanged -= value;
        }

        /// <summary>
        /// �������� ���������� ��������.
        /// </summary>
        /// <returns></returns>
        float IPercentAccessor.Value => Percent;

        /// <summary>
        /// ���������� �������� �������� �� ������� � ���������.
        /// �������� ����������� �� ���������� ���������������.
        /// </summary>
        /// <param name="percent"></param>
        void IPercentMutator.Set(float percent) => SetByPercent(percent);

        float IPercentMutator.SetAndGet(float percent)
        {
            SetByPercent(percent);
            return Percent;
        }

        bool IPercentAccessor.IsMin => IsMin;

        bool IPercentAccessor.IsMax => IsMax;

        void IPercentMutator.SetMax() => SetByPercent(range.Max);

        void IPercentMutator.SetMin() => SetByPercent(range.Min);

        private float Percent => Mathf.InverseLerp(range.Min, range.Max, Value);

        /// <summary>
        /// ���������� �������� �������� �� ������� � ���������.
        /// �������� ����������� �� ���������� ���������������.
        /// </summary>
        /// <param name="percent"></param>s
        public void SetByPercent(float percent)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(range.Min, range.Max, percent));
            Set(value);
        }

        public static IntPercentable operator -(IntPercentable value, int delta)
        {
            value.Set(value.Value - delta);
            return value;
        }

        public static IntPercentable operator -(IntPercentable value, uint delta)
        {
            value.Set((int)(value.Value - delta));
            return value;
        }

        public static IntPercentable operator +(IntPercentable value, int delta)
        {
            value.Set(value.Value + delta);
            return value;
        }

        public static IntPercentable operator +(IntPercentable value, uint delta)
        {
            value.Set((int)(value.Value + delta));
            return value;
        }
    }
}
