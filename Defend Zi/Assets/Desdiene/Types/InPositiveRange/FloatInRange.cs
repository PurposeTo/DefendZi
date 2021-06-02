using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.InPositiveRange.Abstract;
using Desdiene.Types.Range.Positive;

namespace Desdiene.Types.InPositiveRange
{
    public class FloatInRange : ValueInRange<float>, IRef<float>
    {
        public FloatInRange(float value, FloatRange range) : base(value, range) { }

        public static FloatInRange operator -(FloatInRange value, float delta)
        {
            value.Set(value.Get() - delta);
            return value;
        }

        public static FloatInRange operator +(FloatInRange value, float delta)
        {
            value.Set(value.Get() + delta);
            return value;
        }

        public static implicit operator float(FloatInRange value)
        {
            return value.Get();
        }
    }
}
