using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.DataLoaders
{
    public interface IStorageData<T> where T : IData
    {
        string StorageName { get; }

        void Load(Action<T> dataCallback);
        void Save(T data, Action<bool> successCallback);
    }
}
