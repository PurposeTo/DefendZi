namespace Desdiene.Types.PercentAsset
{
    /// <summary>
    /// Сущность, у которой можно взять процентное значение.
    /// </summary>
    public interface IPercentGetter
    {
        bool IsMin { get; }
        bool IsMax { get; }
        float Value { get; }
    }
}
