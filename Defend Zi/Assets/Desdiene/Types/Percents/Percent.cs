using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Ranges.Positive;

namespace Desdiene.Types.Percents
{
    public class Percent : FloatInRange, IPercent
    {
        public Percent() : this(0f) { }

        public Percent(float value) : base(value, new FloatRange(0f, 1f)) { }

        public static Percent operator -(Percent value, float delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static Percent operator +(Percent value, float delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }
    }
}
