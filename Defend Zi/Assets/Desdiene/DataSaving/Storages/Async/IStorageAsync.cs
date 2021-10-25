using System;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Асинхронное хранилище данных.
    /// 
    /// НЕ использовать объект с полученными данными как модель в игровой логике! 
    /// Использовать лишь как data transfer object.
    /// 
    /// Вместо делегата в параметрах метода используются события для того, чтобы избежать утечек памяти.
    /// От события можно отписаться, если коллбек еще не был вызван.
    /// </summary>
    /// <typeparam name="T">Объект с данными</typeparam>
    public interface IStorageAsync<T>
    {
        /// <summary>
        /// bool: успешно? T: данные
        /// </summary>
        event Action<bool, T> OnReaded;

        /// <summary>
        /// bool: успешно?
        /// </summary>
        event Action<bool> OnUpdated;

        /// <summary>
        /// bool: успешно?
        /// </summary>
        event Action<bool> OnDeleted;

        string StorageName { get; }

        /// <summary>
        /// Запустить процесс чтения данных
        /// </summary>
        void Read();

        /// <summary>
        /// Запустить процесс обновления данных
        /// </summary>
        void Update(T data);

        /// <summary>
        /// Запустить процесс удаления данных
        /// </summary>
        void Delete();
    }
}
