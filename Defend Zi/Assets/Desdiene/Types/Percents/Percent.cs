using System;
using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Ranges.Positive;

namespace Desdiene.Types.Percents
{
    public class Percent : FloatInRange, IPercent
    {
        private const float Min = 0f;
        private const float Max = 1f;

        public Percent() : this(0f) { }

        public Percent(float value) : base(value, new FloatRange(Min, Max)) { }

        event Action IPercentNotifier.OnChanged
        {
            add => valueRef.OnChanged += value;
            remove => valueRef.OnChanged -= value;
        }

        bool IPercentAccessor.IsMin => IsMin;

        bool IPercentAccessor.IsMax => IsMax;

        float IPercentAccessor.Value => Value;

        void IPercentMutator.Set(float percent) => Set(percent);

        void IPercentMutator.SetMax() => Set(Max);

        void IPercentMutator.SetMin() => Set(Min);

        float IPercentMutator.SetAndGet(float percent)
        {
            Set(percent);
            return Value;
        }

        public static Percent operator -(Percent value, float delta)
        {
            value.Set(value.Value - delta);
            return value;
        }

        public static Percent operator +(Percent value, float delta)
        {
            value.Set(value.Value + delta);
            return value;
        }
    }
}
