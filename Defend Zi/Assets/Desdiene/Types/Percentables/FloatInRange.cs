using System;
using Desdiene.Types.Percentables.Base;
using Desdiene.Types.Percentale;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

namespace Desdiene.Types.Percentables
{
    [Serializable]
    public class FloatInRange : ValueInRange<float>, IPercentable<float>
    {
        public FloatInRange(float value, FloatRange range) : base(value, range) { }

        event Action IPercentNotifier.OnChanged
        {
            add => valueRef.OnChanged += value;
            remove => valueRef.OnChanged -= value;
        }

        /// <summary>
        /// Получить процентное значение.
        /// </summary>
        /// <returns></returns>
        float IPercentAccessor.Value => Percent;

        bool IPercentAccessor.IsMin => IsMin;

        bool IPercentAccessor.IsMax => IsMax;

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>
        void IPercentMutator.Set(float percent) => SetByPercent(percent);

        float IPercentMutator.SetAndGet(float percent)
        {
            SetByPercent(percent);
            return Percent;
        }

        void IPercentMutator.SetMax() => SetByPercent(1f);

        void IPercentMutator.SetMin() => SetByPercent(0f);

        private float Percent => Mathf.InverseLerp(range.Min, range.Max, Value);

        protected override bool IsMin => Mathf.Approximately(Value, range.Min);
        protected override bool IsMax => Mathf.Approximately(Value, range.Max);

        /// <summary>
        /// Установить значение опираясь на процент в диапазоне.
        /// Значение округляется до ближайшего целочисленноого.
        /// </summary>
        /// <param name="percent"></param>s
        private void SetByPercent(float percent)
        {
            float value = Mathf.Lerp(range.Min, range.Max, percent);
            Set(value);
        }

        public static FloatInRange operator -(FloatInRange value, float delta)
        {
            value.Set(value.Value - delta);
            return value;
        }

        public static FloatInRange operator +(FloatInRange value, float delta)
        {
            value.Set(value.Value + delta);
            return value;
        }
    }
}
