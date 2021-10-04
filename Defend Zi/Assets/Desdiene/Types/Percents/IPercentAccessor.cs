namespace Desdiene.Types.Percents
{
    /// <summary>
    /// Сущность, у которой можно взять процентное значение.
    /// </summary>
    public interface IPercentAccessor
    {
        bool IsMin { get; }
        bool IsMax { get; }
        float Value { get; }
    }
}
