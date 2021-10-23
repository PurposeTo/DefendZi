using System;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Асинхронное хранилище данных.
    /// 
    /// НЕ использовать объект с полученными данными как модель в игровой логике! 
    /// Использовать лишь как data transfer object.
    /// </summary>
    /// <typeparam name="T">Объект с данными</typeparam>
    public interface IStorageAsync<T>
    {
        string StorageName { get; }

        void Read(Action<bool, T> result);
        void Update(T data, Action<bool> successResult);
        void Delete(Action<bool> successResult);
    }
}
