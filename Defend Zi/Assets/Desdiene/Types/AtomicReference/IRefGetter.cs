namespace Desdiene.Types.AtomicReference
{
    /// <summary>
    /// Интерфейс для чтения значения и получении уведомления о его изменении.
    /// </summary>
    /// <typeparam name="T">Тип значения.</typeparam>
    public interface IRefGetter<T>
    {
        T Get();
    }
}
