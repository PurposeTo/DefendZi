using Desdiene.Types.InPositiveRange;
using Desdiene.Types.PercentAsset;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentable
{
    public class FloatPercentable : FloatInRange, IPercentable<float>
    {
        public FloatPercentable(float value, FloatRange range) : base(value, range) { }

        /// <summary>
        /// �������� ���������� ��������.
        /// </summary>
        /// <returns></returns>
        float IPercentGetter.Get() => GetPercent();

        /// <summary>
        /// ���������� �������� �������� �� ������� � ���������.
        /// �������� ����������� �� ���������� ���������������.
        /// </summary>
        /// <param name="percent"></param>
        void IPercentSetter.Set(float percent) => SetByPercent(percent);

        float IPercentSetter.SetAndGet(float percent) => SetByPercentAndGet(percent);

        public float GetPercent() => Mathf.InverseLerp(range.Min, range.Max, Get());

        /// <summary>
        /// ���������� �������� �������� �� ������� � ���������.
        /// �������� ����������� �� ���������� ���������������.
        /// </summary>
        /// <param name="percent"></param>s
        public void SetByPercent(float percent)
        {
            float value = Mathf.Lerp(range.Min, range.Max, percent);
            Set(value);
        }

        public float SetByPercentAndGet(float percent)
        {
            SetByPercent(percent);
            return GetPercent();
        }

        public static FloatPercentable operator -(FloatPercentable value, float delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static FloatPercentable operator +(FloatPercentable value, float delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }
    }
}
