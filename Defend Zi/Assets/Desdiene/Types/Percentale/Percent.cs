using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.Percentable
{
    public class Percent : FloatInRange, IRef<float>
    {
        public Percent() : this(0f) { }

        public Percent(float value) : base(value, new FloatRange(0f, 1f)) { }
    }
}
