namespace Desdiene.Types.Percent
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
