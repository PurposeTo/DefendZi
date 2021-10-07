using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.DataLoaders
{
    internal class LoadersContainer<T> : IDataLoader<T> where T : IData
    {
        private readonly IDataLoader<T>[] storages;

        public LoadersContainer(params IDataLoader<T>[] storages)
        {
            this.storages = storages;
        }

        string IDataLoader<T>.StorageName => "Контейнер загрузчиков данных"; // todo перечислить имена всех загрузчиков

        void IDataLoader<T>.Load(Action<T> data)
        {
            Array.ForEach(storages, storage => storage.Load(data));
        }

        void IDataLoader<T>.Save(T data)
        {
            Array.ForEach(storages, storage => storage.Save(data));
        }
    }
}
