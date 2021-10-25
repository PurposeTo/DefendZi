using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.DataSaving.Datas;

namespace Desdiene.DataSaving.Storages
{
    internal class StoragesAsyncContainer<T> : IStorageAsync<T> where T : IDataWithPlayingTime
    {
        private readonly string _typeName;
        private readonly string _storageNames;
        private readonly IStorageAsync<T>[] _storages;
        // загруженные данные, полученные с хранилищ/а
        private KeyValuePair<TimeSpan, int> _hashLoadedData = new KeyValuePair<TimeSpan, int>();

        // список параметров может быть пустым, если в игру пока не интегрированно сохранение.
        public StoragesAsyncContainer(params IStorageAsync<T>[] storages)
        {
            _storages = storages;
            _typeName = GetType().Name;
            _storageNames = string.Join("\n", _storages.Select(it => it.StorageName));
            SubscribeEvents();
        }

        private event Action<bool, T> OnReaded;
        private event Action<bool> OnUpdated;
        private event Action<bool> OnDeleted;

        event Action<bool, T> IStorageAsync<T>.OnReaded
        {
            add => OnReaded += value;
            remove => OnReaded -= value;
        }

        event Action<bool> IStorageAsync<T>.OnUpdated
        {
            add => OnUpdated += value;
            remove => OnUpdated -= value;
        }

        event Action<bool> IStorageAsync<T>.OnDeleted
        {
            add => OnDeleted += value;
            remove => OnDeleted -= value;
        }

        string IStorageAsync<T>.StorageName => $"[{_typeName}] Контейнер хранилищ:\n{_storageNames}";

        /// <summary>
        /// Перенаправить запрос на загрузку данных в хранилища.
        /// Коллбек будет вызван <= раз, относительно количества хранилищ.
        /// 
        /// При не совпадении данных с хранилищ, будет автоматическое разрешение конфликта:
        /// Будут выбраны данные с наибольшем значением TimeSpan playingTime.
        /// </summary>
        /// <param name="result">bool: Успешно? T: данные</param>
        void IStorageAsync<T>.Read()
        {
            Array.ForEach(_storages, storage => storage.Read());
        }

        /// <summary>
        /// Перенаправить запрос на сохранение данных в хранилища.
        /// Коллбек будет вызван <= раз, относительно количества хранилищ.
        /// </summary>
        /// <param name="data">Сохраняемые данные</param>
        /// <param name="successResult">Успешно?</param>
        void IStorageAsync<T>.Update(T data)
        {
            Array.ForEach(_storages, storage => storage.Update(data));
        }

        /// <summary>
        /// Перенаправить запрос на удаление данных в хранилища.
        /// Коллбек будет вызван <= раз, относительно количества хранилищ.
        /// </summary>
        /// <param name="successResult">Успешно?</param>
        void IStorageAsync<T>.Delete()
        {
            Array.ForEach(_storages, storage => storage.Delete());
        }

        private bool IsNewDataMoreRelevant(T newData) => newData.TotalLifeTime > _hashLoadedData.Key;
        private bool IsNotDataEquals(T newData) => newData.GetHashCode() != _hashLoadedData.Value;

        private void SubscribeEvents()
        {
            Array.ForEach(_storages, storage => SubscribeEvents(storage));
        }

        private void SubscribeEvents(IStorageAsync<T> storage)
        {
            storage.OnReaded += InvokeOnReadedWithConflictResolution;
            storage.OnUpdated += InvokeOnUpdated;
            storage.OnDeleted += InvokeOnDeleted;
        }

        private void InvokeOnReadedWithConflictResolution(bool success, T data)
        {
            if (!success)
            {
                OnReaded?.Invoke(success, data);
                return;
            }

            bool isHashLoadedDataEmpty = _hashLoadedData.Equals(default(KeyValuePair<TimeSpan, int>));

            // если первое предыдущее условие не выполнено, то и след. не должно выполняться.
            if (isHashLoadedDataEmpty || (IsNewDataMoreRelevant(data) && IsNotDataEquals(data)))
            {
                _hashLoadedData = new KeyValuePair<TimeSpan, int>(data.TotalLifeTime, data.GetHashCode());
                OnReaded?.Invoke(success, data);
            }
            // else: ничего не делать.
        }

        private void InvokeOnUpdated(bool success) => OnUpdated?.Invoke(success);
        private void InvokeOnDeleted(bool success) => OnDeleted?.Invoke(success);
    }
}
