namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Хранилище данных. Методы выполняются в синхронном потоке.
    /// 
    /// НЕ использовать объект с полученными данными как модель в игровой логике! 
    /// Использовать лишь как data transfer object.
    /// </summary>
    /// <typeparam name="T">Объект с данными</typeparam>
    public interface IStorage<T>
    {
        string StorageName { get; }

        /// <returns>успешно?</returns>
        bool TryToRead(out T data);


        /// <returns>успешно?</returns>
        bool TryToUpdate(T data);

        /// <returns>успешно?</returns>
        bool TryToDelete();
    }
}
