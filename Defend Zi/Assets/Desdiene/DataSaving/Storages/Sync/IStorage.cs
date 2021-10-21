namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Хранилище данных.
    /// 
    /// НЕ использовать объект с полученными данными как модель в игровой логике! 
    /// Использовать лишь как data transfer object.
    /// </summary>
    /// <typeparam name="T">Объект с данными</typeparam>
    public interface IStorage<T>
    {
        string StorageName { get; }

        /// <returns>успешно?</returns>
        bool TryToLoad(out T data);


        /// <returns>успешно?</returns>
        bool Save(T data);

        /// <returns>успешно?</returns>
        bool TryToClean();
    }
}
