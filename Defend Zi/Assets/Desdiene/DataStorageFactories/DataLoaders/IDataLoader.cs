using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.DataLoaders
{
    public interface IDataLoader<T> where T : IData
    {
        string StorageName { get; }

        void Load(Action<T> dataCallback);
        void Save(T data);
    }
}
