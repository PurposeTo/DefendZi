using System;

namespace Desdiene.DataSaving.Storages
{
    public interface IStorageAsync<T>
    {
        string StorageName { get; }

        void Load(Action<bool, T> result);
        void Save(T data, Action<bool> successResult);
        void Clean(Action<bool> successResult);
    }
}
