using Desdiene.Types.InPositiveRange;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.Percent
{
    //���������� ������ percent'S' ��� ��������� ��������� ���� � package.
    public class Percents : FloatInRange, IPercent
    {
        public Percents() : this(0f) { }

        public Percents(float value) : base(value, new FloatRange(0f, 1f)) { }
    }
}
