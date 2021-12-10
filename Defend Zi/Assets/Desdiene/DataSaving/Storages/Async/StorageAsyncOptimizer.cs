using System;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Оптимизация отправки запросов в хранилище путем кеширования данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StorageAsyncOptimizer<T> : IStorageAsync<T> where T : class, ICloneable, new()
    {
        private readonly IStorageAsync<T> _storage;

        private T _cachedData;
        private bool _isCachedDataInited;

        public StorageAsyncOptimizer(IStorageAsync<T> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            SubscribeEvents();
        }

        private event Action<bool, T> OnReaded;
        private event Action<bool> OnUpdated;
        private event Action<bool> OnDeleted;

        string IStorageAsync<T>.StorageName => _storage.StorageName;

        event Action<bool, T> IStorageAsync<T>.OnReaded
        {
            add => OnReaded += value;
            remove => OnReaded -= value;
        }

        event Action<bool> IStorageAsync<T>.OnUpdated
        {
            add => OnUpdated += value;
            remove => OnUpdated -= value;
        }

        event Action<bool> IStorageAsync<T>.OnDeleted
        {
            add => OnDeleted += value;
            remove => OnDeleted -= value;
        }

        void IStorageAsync<T>.Read()
        {
            if (_isCachedDataInited)
            {
                OnReaded?.Invoke(true, _cachedData);
                return;
            }

            void CaсheData(bool success, T data)
            {
                CacheData(success, data.Clone() as T);
                OnReaded -= CaсheData;
            }

            OnReaded += CaсheData;
            _storage.Read();
        }

        void IStorageAsync<T>.Update(T data)
        {
            if (_isCachedDataInited && Equals(data, _cachedData))
            {
                OnUpdated?.Invoke(true);
                return;
            }

            void CaсheData(bool success)
            {
                CacheData(success, data.Clone() as T);
                OnUpdated -= CaсheData;
            }

            OnUpdated += CaсheData;
            _storage.Update(data);
        }

        void IStorageAsync<T>.Delete()
        {
            // Запрос на удаление должен быть отправлен в любом случае, чтобы в хранилище данных не было совсем, а не были "пустые данные"

            void CaсheData(bool success)
            {
                CacheData(success, new T());
                OnDeleted -= CaсheData;
            }

            OnDeleted += CaсheData;
            _storage.Delete();
        }

        private void SubscribeEvents()
        {
            _storage.OnReaded += (_1, _2) => OnReaded?.Invoke(_1, _2);
            _storage.OnUpdated += (_1) => OnUpdated?.Invoke(_1);
            _storage.OnDeleted += (_1) => OnDeleted?.Invoke(_1);
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

    public static class StorageAsyncOptimizerExt
    {
        public static IStorageAsync<T> Optimize<T>(this IStorageAsync<T> storage) where T : class, ICloneable, new()
        {
            return new StorageAsyncOptimizer<T>(storage);
        }
    }
}
