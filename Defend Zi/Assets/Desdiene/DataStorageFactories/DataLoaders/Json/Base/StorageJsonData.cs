using System;
using Desdiene.Containers;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.JsonConvertors;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Json
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с хранилища.
    /// Логика загрузки и сохранения данных на само хранилище определяется в дочернем классе.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StorageJsonData<T> : MonoBehaviourExtContainer, IStorageData<T> where T : IData, new()
    {
        private readonly string _storageName;
        private readonly IJsonConvertor<T> _jsonConvertor;

        public StorageJsonData(MonoBehaviourExt mono,
            string storageName,
            string fileName,
            IJsonConvertor<T> jsonConvertor)
            : base(mono)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"\"{nameof(fileName)}\" can't be null or empty");
            }

            if (jsonConvertor is null)
            {
                throw new ArgumentNullException(nameof(jsonConvertor));
            }

            FileName = fileName;
            _storageName = storageName;
            _jsonConvertor = new JsonConvertorValidator<T>(jsonConvertor);
        }

        string IStorageData<T>.StorageName => _storageName;
        protected string FileName { get; }
        protected string FileExtension => "json";
        protected string FileNameWithExtension => FileName + "." + FileExtension;

        /// <summary>
        /// Возвращает коллбеком данные из хранилища.
        /// Не вызовется, если произошли проблемы при чтении.
        /// </summary>
        /// <param name="dataCallback"></param>
        void IStorageData<T>.Load(Action<T> dataCallback)
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

        void IStorageData<T>.Save(T data, Action<bool> successCallback)
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
