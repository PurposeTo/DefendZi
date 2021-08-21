using System;
using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Percentale;
using Desdiene.Types.PercentAsset;
using Desdiene.Types.Range.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentable
{
    public class IntPercentable : IntInRange, IPercentable<int>
    {
        public IntPercentable(int value, IntRange range) : base(value, range) { }

        event Action IPercentNotifier.OnValueChanged
        {
            add => OnValueChanged += value;
            remove => OnValueChanged -= value;
        }

        /// <summary>
        /// Получить процентное значение.
        /// </summary>
        /// <returns></returns>
        float IPercentGetter.Value => Percent;

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        void IPercentSetter.Set(float percent) => SetByPercent(percent);

        float IPercentSetter.SetAndGet(float percent)
        {
            SetByPercent(percent);
            return Percent;
        }

        public float Percent => Mathf.InverseLerp(range.Min, range.Max, Get());

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>s
        public void SetByPercent(float percent)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(range.Min, range.Max, percent));
            Set(value);
        }

        public static IntPercentable operator -(IntPercentable value, int delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static IntPercentable operator -(IntPercentable value, uint delta)
        {
            value.Set((int)(value.Get() - delta));
            return value;
        }

        public static IntPercentable operator +(IntPercentable value, int delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }

        public static IntPercentable operator +(IntPercentable value, uint delta)
        {
            value.Set((int)(value.Get() + delta));
            return value;
        }
    }
}
