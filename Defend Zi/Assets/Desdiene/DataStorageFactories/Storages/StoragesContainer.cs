﻿using System;
using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.Storages
{
    internal class StoragesContainer<T> : IStorageData<T> where T : IData
    {
        private readonly IStorageData<T>[] storages;

        public StoragesContainer(params IStorageData<T>[] storages)
        {
            this.storages = storages;
        }

        string IStorageData<T>.StorageName => "Контейнер загрузчиков данных"; // todo перечислить имена всех загрузчиков

        void IStorageData<T>.Load(Action<T> data)
        {
            Array.ForEach(storages, storage => storage.Load(data));
        }

        void IStorageData<T>.Save(T data, Action<bool> successCallback)
        {
            Array.ForEach(storages, storage => storage.Save(data, successCallback));
        }
    }
}