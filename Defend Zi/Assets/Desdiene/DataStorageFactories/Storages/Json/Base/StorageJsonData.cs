using System;
using Desdiene.Containers;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages.Json
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с хранилища.
    /// Логика загрузки и сохранения данных на само хранилище определяется в дочернем классе.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StorageJsonData<T> : MonoBehaviourExtContainer, IDataStorageOld<T> where T : IData, new()
    {
        private readonly string _storageName;
        private readonly IJsonConvertor<T> _jsonConvertor;

        public StorageJsonData(MonoBehaviourExt mono,
            string storageName,
            string fileNameWoExt,
            IJsonConvertor<T> jsonConvertor)
            : base(mono)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(fileNameWoExt))
            {
                throw new ArgumentException($"\"{nameof(fileNameWoExt)}\" can't be null or empty");
            }

            if (jsonConvertor is null)
            {
                throw new ArgumentNullException(nameof(jsonConvertor));
            }

            FileName = fileNameWoExt;
            _storageName = storageName;
            _jsonConvertor = new JsonConvertorValidator<T>(jsonConvertor);
        }

        string IDataStorageOld<T>.StorageName => _storageName;
        protected string FileName { get; }
        protected string FileExtension => "json";
        protected string FileNameWithExtension => FileName + "." + FileExtension;

        /// <summary>
        /// Возвращает коллбеком данные из хранилища.
        /// Не вызовется, если произошли проблемы при чтении.
        /// </summary>
        /// <param name="dataCallback"></param>
        void IDataStorageOld<T>.Load(Action<T> dataCallback)
        {
            Debug.Log($"Начата загрузка данных с [{_storageName}]");
            LoadJsonData(jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.Log($"Данные на [{_storageName}] не найдены");
                    dataCallback?.Invoke(new T());
                }
                else
                {
                    Debug.Log($"Данные с [{_storageName}] загружены\nДанные:\n{jsonData}");
                    T data = _jsonConvertor.Deserialize(jsonData);
                    data.TryToRepair();
                    dataCallback?.Invoke(data);
                }
            });
        }

        void IDataStorageOld<T>.Save(T data, Action<bool> successCallback)
        {
            Debug.Log($"Начато сохранение данных на [{_storageName}]");
            if (data.IsValid())
            {
                string jsonData = _jsonConvertor.Serialize(data);
                SaveJsonData(jsonData, successCallback);
            }
            else Debug.LogError($"Data is not valid!\n{data}");
        }

        protected abstract void LoadJsonData(Action<string> jsonDataCallback);

        protected abstract void SaveJsonData(string jsonData, Action<bool> successCallback);
    }
}
