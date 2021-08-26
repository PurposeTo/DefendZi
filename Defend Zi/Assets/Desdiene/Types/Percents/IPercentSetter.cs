namespace Desdiene.Types.Percents
{
    /// <summary>
    /// Сущность, которой можно записать процентное значние.
    /// </summary>
    public interface IPercentSetter
    {
        void Set(float percent);
        float SetAndGet(float percent);
    }
}
