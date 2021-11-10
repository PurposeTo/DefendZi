using System;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public class StorageAsyncLogger<T> : IStorageAsync<T>
    {
        private readonly IStorageAsync<T> _storage;

        public StorageAsyncLogger(IStorageAsync<T> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            SubscribeEvents();
        }

        string IStorageAsync<T>.StorageName => _storage.StorageName;

        event Action<bool, T> IStorageAsync<T>.OnReaded
        {
            add => _storage.OnReaded += value;
            remove => _storage.OnReaded -= value;
        }

        event Action<bool> IStorageAsync<T>.OnUpdated
        {
            add => _storage.OnUpdated += value;
            remove => _storage.OnUpdated -= value;
        }

        event Action<bool> IStorageAsync<T>.OnDeleted
        {
            add => _storage.OnDeleted += value;
            remove => _storage.OnDeleted -= value;
        }

        void IStorageAsync<T>.Read()
        {
            Debug.Log($"Invoking data reading from [{_storage.StorageName}]");
            _storage.Read();
        }

        void IStorageAsync<T>.Update(T data)
        {
            Debug.Log($"Invoking data updating from [{_storage.StorageName}]");
            _storage.Update(data);
        }

        void IStorageAsync<T>.Delete()
        {
            Debug.Log($"Invoking data deleting from [{_storage.StorageName}]");
            _storage.Delete();
        }

        private void SubscribeEvents()
        {
            _storage.OnReaded += LogDataReadedResult;
            _storage.OnUpdated += LogDataUpdatedResult;
            _storage.OnDeleted += LogDataDeletedResult;
        }

        private void LogDataReadedResult(bool success, T data)
        {
            Debug.Log($"Data was readed from [{_storage.StorageName}] with success status: {success}");
        }

        private void LogDataUpdatedResult(bool success)
        {
            Debug.Log($"Data was updated from [{_storage.StorageName}] with success status: {success}");
        }

        private void LogDataDeletedResult(bool success)
        {
            Debug.Log($"Data was deleted from [{_storage.StorageName}] with success status: {success}");
        }
    }
}
