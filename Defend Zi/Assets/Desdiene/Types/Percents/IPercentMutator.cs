namespace Desdiene.Types.Percents
{
    /// <summary>
    /// Сущность, которой можно записать процентное значние.
    /// </summary>
    public interface IPercentMutator
    {
        void Set(float percent);
        void SetMax();
        void SetMin();
        float SetAndGet(float percent);
    }
}
