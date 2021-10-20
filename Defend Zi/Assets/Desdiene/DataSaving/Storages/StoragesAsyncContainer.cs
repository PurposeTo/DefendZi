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
        void IStorageAsync<T>.Load(Action<bool, T> result)
        {
            Array.ForEach(_storages, storage => LoadWithConflictResolution(storage, result));
        }

        /// <summary>
        /// Перенаправить запрос на сохранение данных в хранилища.
        /// Коллбек будет вызван <= раз, относительно количества хранилищ.
        /// </summary>
        /// <param name="data">Сохраняемые данные</param>
        /// <param name="successResult">Успешно?</param>
        void IStorageAsync<T>.Save(T data, Action<bool> successResult)
        {
            Array.ForEach(_storages, storage => storage.Save(data, successResult));
        }

        /// <summary>
        /// Перенаправить запрос на удаление данных в хранилища.
        /// Коллбек будет вызван <= раз, относительно количества хранилищ.
        /// </summary>
        /// <param name="successResult">Успешно?</param>
        void IStorageAsync<T>.Clean(Action<bool> successResult)
        {
            Array.ForEach(_storages, storage => storage.Clean(successResult));
        }

        private void LoadWithConflictResolution(IStorageAsync<T> storage, Action<bool, T> result)
        {
            storage.Load((success, data) =>
            {
                if (!success) result?.Invoke(success, data);

                bool isHashLoadedDataEmpty = _hashLoadedData.Equals(default(KeyValuePair<TimeSpan, int>));

                // если первое предыдущее условие не выполнено, то и след. не должно выполняться.
                if (isHashLoadedDataEmpty || (IsNewDataMoreRelevant(data) && isNotDataEquals(data)))
                {
                    _hashLoadedData = new KeyValuePair<TimeSpan, int>(data.PlayingTime, data.GetHashCode());
                    result?.Invoke(success, data);
                }
                // else: ничего не делать.
            });
        }

        private bool IsNewDataMoreRelevant(T newData) => newData.PlayingTime > _hashLoadedData.Key;
        private bool isNotDataEquals(T newData) => newData.GetHashCode() != _hashLoadedData.Value;
    }
}
