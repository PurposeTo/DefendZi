using System;
using Desdiene.DataSaving.Datas;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract class Storage<T> : IStorage<T> where T : IValidData
    {
        private readonly string _storageName;

        protected Storage(string storageName)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} can't be null or empty");
            }

            _storageName = storageName;
        }

        string IStorage<T>.StorageName => _storageName;

        bool IStorage<T>.TryLoad(out T data)
        {
            try
            {
                if (TryLoad(out data))
                {
                    data.TryToRepair();
                    return true;
                }
                else
                {
                    data = default;
                    return false;
                }

            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                data = default;
                return false;
            }
        }

        bool IStorage<T>.Save(T data)
        {
            if (!data.IsValid())
            {
                Debug.LogError($"Data is not valid!\n{data}");
                return false;
            }

            try
            {
                return Save(data);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return false;
            }
        }

        bool IStorage<T>.TryClean()
        {
            try
            {
                return TryClean();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return false;
            }
        }

        protected abstract bool TryLoad(out T data);
        protected abstract bool Save(T data);
        protected abstract bool TryClean();
    }
}
