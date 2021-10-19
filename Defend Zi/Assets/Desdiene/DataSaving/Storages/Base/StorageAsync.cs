using System;
using Desdiene.DataSaving.Datas;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract class StorageAsync<T> : IStorageAsync<T> where T : IValidData
    {
        private readonly string _storageName;

        protected StorageAsync(string storageName)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} can't be null or empty");
            }

            _storageName = storageName;
        }

        string IStorageAsync<T>.StorageName => _storageName;

        void IStorageAsync<T>.Load(Action<bool, T> result)
        {
            try
            {
                Load((success, data) =>
                {
                    if (success) data.TryToRepair();
                    result?.Invoke(success, data);
                });
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                result?.Invoke(false, default);
            }
        }

        void IStorageAsync<T>.Save(T data, Action<bool> successResult)
        {
            if (!data.IsValid())
            {
                Debug.LogError($"Data is not valid!\n{data}");
                successResult?.Invoke(false);
            }

            try
            {
                Save(data, successResult);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                successResult?.Invoke(false);
            }
        }

        void IStorageAsync<T>.Clean(Action<bool> successResult) => Clean(successResult);

        protected abstract void Load(Action<bool, T> result);
        protected abstract void Save(T data, Action<bool> successResult);
        protected abstract void Clean(Action<bool> successResult);
    }
}
