using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.Storages
{
    internal class StoragesContainer<T> : IDataStorageOld<T> where T : IData
    {
        private readonly IDataStorageOld<T>[] storages;

        public StoragesContainer(params IDataStorageOld<T>[] storages)
        {
            this.storages = storages;
        }

        string IDataStorageOld<T>.StorageName => "Контейнер загрузчиков данных"; // todo перечислить имена всех загрузчиков

        void IDataStorageOld<T>.Load(Action<T> data)
        {
            Array.ForEach(storages, storage => storage.Load(data));
        }

        void IDataStorageOld<T>.Save(T data, Action<bool> successCallback)
        {
            Array.ForEach(storages, storage => storage.Save(data, successCallback));
        }
    }
}
