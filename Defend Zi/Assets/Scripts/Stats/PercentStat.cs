public class PercentStat : FloatStatClamp, IStat<float>
{
    public PercentStat() : this(0f) { }

    public PercentStat(float value) : base(value, 0f, 1f) { }
}
