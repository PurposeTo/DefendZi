using Desdiene.Types.AtomicReference.Api;
using Desdiene.Types.RangeType;

namespace Desdiene.Types.ValuesInRange
{
    public class Percent : FloatInRange, IRef<float>
    {
        public Percent() : this(0f) { }

        public Percent(float value) : base(value, new Range<float>(0f, 1f)) { }
    }
}
