using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.Percent
{
    //называется именно percent'S' для изюежания конфликта имен с package.
    public class Percents : FloatInRange, IPercent
    {
        public Percents() : this(0f) { }

        public Percents(float value) : base(value, new FloatRange(0f, 1f)) { }
    }
}
