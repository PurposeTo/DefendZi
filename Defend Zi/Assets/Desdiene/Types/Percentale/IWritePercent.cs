namespace Desdiene.Types.Percentable
{
    /// <summary>
    /// Сущность, которой можно записать процентное значние.
    /// </summary>
    public interface IWritePercent
    {
        void Set(float percent);
        float SetAndGet(float percent);
    }
}
