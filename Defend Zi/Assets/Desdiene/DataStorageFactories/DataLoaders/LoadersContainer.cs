using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.DataLoaders
{
    internal class LoadersContainer<T> : IStorageDataLoader<T> where T : IData
    {
        private readonly IStorageDataLoader<T>[] storages;

        public LoadersContainer(params IStorageDataLoader<T>[] storages)
        {
            this.storages = storages;
        }

        public void Load(Action<T> data)
        {
            Array.ForEach(storages, storage => storage.Load(data));
        }

        public void Save(T data)
        {
            Array.ForEach(storages, storage => storage.Save(data));
        }
    }
}
