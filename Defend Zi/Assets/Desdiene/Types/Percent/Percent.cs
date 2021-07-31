using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.PercentAsset
{
    public class Percent : FloatInRange, IPercent
    {
        public Percent() : this(0f) { }

        public Percent(float value) : base(value, new FloatRange(0f, 1f)) { }
    }
}
