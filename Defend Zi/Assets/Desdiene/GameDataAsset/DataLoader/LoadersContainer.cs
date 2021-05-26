using System;
using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.DataLoader
{
    internal class LoadersContainer<T> : 
        IStorageDataLoader<T>
        where T : GameData
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
