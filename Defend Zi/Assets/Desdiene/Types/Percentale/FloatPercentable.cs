using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Percent;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentable
{
    public class FloatPercentable : FloatInRange, IPercentable<float>
    {
        public FloatPercentable(float value, FloatRange range) : base(value, range) { }

        /// <summary>
        /// Получить процентное значение.
        /// </summary>
        /// <returns></returns>
        float IReadPercent.Get() => GetPercent();

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        void IWritePercent.Set(float percent) => SetByPercent(percent);

        float IWritePercent.SetAndGet(float percent) => SetByPercentAndGet(percent);

        public float GetPercent() => Mathf.InverseLerp(range.Min, range.Max, Get());

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
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
