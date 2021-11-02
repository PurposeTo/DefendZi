using System;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public class StorageLogger<T> : IStorage<T>
    {
        private readonly IStorage<T> _storage;

        public StorageLogger(IStorage<T> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        string IStorage<T>.StorageName => _storage.StorageName;

        bool IStorage<T>.TryToRead(out T data)
        {
            bool success = _storage.TryToRead(out data);
            Debug.Log($"Data was readed from [{_storage.StorageName}] with success status: {success}");
            return success;
        }

        bool IStorage<T>.TryToUpdate(T data)
        {
            bool success = _storage.TryToUpdate(data);
            Debug.Log($"Data was updated from [{_storage.StorageName}] with success status: {success}");
            return success;
        }

        bool IStorage<T>.TryToDelete()
        {
            bool success = _storage.TryToDelete();
            Debug.Log($"Data was deleted from [{_storage.StorageName}] with success status: {success}");
            return success;
        }
    }
}
