using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.Storages
{
    public interface IDataStorageOld<T> where T : IData
    {
        string StorageName { get; }

        void Load(Action<T> dataCallback);
        void Save(T data, Action<bool> successCallback);
    }
}
