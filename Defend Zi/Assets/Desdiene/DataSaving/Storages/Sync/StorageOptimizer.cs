using System;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Оптимизация отправки запросов в хранилище путем кеширования данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StorageOptimizer<T> : IStorage<T> where T : class, ICloneable, new()
    {
        private readonly IStorage<T> _storage;

        private T _cachedData;
        private bool _isCachedDataInited;

        public StorageOptimizer(IStorage<T> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        string IStorage<T>.StorageName => _storage.StorageName;

        bool IStorage<T>.TryToRead(out T data)
        {
            if (_isCachedDataInited)
            {
                data = _cachedData;
                return true;
            }

            bool success = _storage.TryToRead(out data);
            CacheData(success, data.Clone() as T);

            return success;
        }

        bool IStorage<T>.TryToUpdate(T data)
        {
            if (_isCachedDataInited && Equals(data, _cachedData)) return true;

            bool success = _storage.TryToUpdate(data);
            CacheData(success, data.Clone() as T);

            return success;
        }

        bool IStorage<T>.TryToDelete()
        {
            // Запрос на удаление должен быть отправлен в любом случае, чтобы в хранилище данных не было совсем, а не были "пустые данные"
            bool success = _storage.TryToDelete();
            CacheData(success, new T());

            return success;
        }

        private void CacheData(bool success, T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            if (success)
            {
                _cachedData = data;
                _isCachedDataInited = true;
            }
        }
    }

    public static class StorageOptimizerExt
    {
        public static IStorage<T> Optimize<T>(this IStorage<T> storage) where T : class, ICloneable, new()
        {
            return new StorageOptimizer<T>(storage);
        }
    }
}