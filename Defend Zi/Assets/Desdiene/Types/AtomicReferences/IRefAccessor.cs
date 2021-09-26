namespace Desdiene.Types.AtomicReferences
{
    /// <summary>
    /// Интерфейс для чтения значения и получении уведомления о его изменении.
    /// </summary>
    /// <typeparam name="T">Тип значения.</typeparam>
    public interface IRefAccessor<T>
    {
        T Value { get; }
    }
}
