using System;
using Desdiene.Container;
using Desdiene.GameDataAsset.Data;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.FromStorage
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с хранилища.
    /// Логика загрузки и сохранения данных на само хранилище определяется в дочернем классе.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StorageJsonDataLoader<T> : MonoBehaviourExtContainer, IStorageDataLoader<T> where T : IData, new()
    {
        private readonly Validator _validator = new Validator();
        private readonly IJsonConvertor<T> _jsonConvertor;

        /// <param name="storageName">Имя хранилища</param>
        /// <param name="fileName">Имя сохраняемого файла</param>
        /// <param name="fileName">расширение сохраняемого файла</param>
        /// <param name="jsonConvertor">json (де)сериализатор</param>
        public StorageJsonDataLoader(MonoBehaviourExt mono,
            string storageName,
            string fileName,
            IJsonConvertor<T> jsonConvertor)
            : base(mono)
        {
            if (string.IsNullOrEmpty(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} не может быть пустым или иметь значение null");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} не может быть пустым или иметь значение null");
            }

            StorageName = storageName;
            FileName = fileName;
            _jsonConvertor = jsonConvertor ?? throw new ArgumentNullException(nameof(jsonConvertor));
        }

        public string StorageName { get; }
        protected string FileName { get; }
        protected string FileExtension => "json";
        protected string FileNameWithExtension => FileName + "." + FileExtension;

        /// <summary>
        /// Возвращает коллбеком данные из хранилища.
        /// Не вызовется, если произошли проблемы при чтении.
        /// </summary>
        /// <param name="dataCallback"></param>
        public void Load(Action<T> dataCallback)
        {
            Debug.Log($"Начата загрузка данных с [{StorageName}]");
            ReadFromStorage(jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.Log($"Данные на [{StorageName}] не найдены");
                    dataCallback?.Invoke(new T());
                }
                else
                {
                    Debug.Log($"Данные с [{StorageName}] загружены\nДанные:\n{jsonData}");
                    T data = DeserializeData(jsonData);
                    data = TryToRepairNullFields(data);
                    dataCallback?.Invoke(data);
                }
            });
        }

        public void Save(T data)
        {
            Debug.Log($"Начато сохранение данных на [{StorageName}]");
            string jsonData = SerializeData(data);
            if (_validator.HasJsonNullValues(jsonData)) return;
            else
            {
                WriteToStorage(jsonData);
            }
        }

        protected abstract void ReadFromStorage(Action<string> jsonDataCallback);

        protected abstract void WriteToStorage(string jsonData);

        /// <summary>
        /// Установить значения полям, которые is null.
        /// Данная реализация создает экземпляр класса T.
        /// </summary>
        /// <param name="data">Объект, содержащий null поля</param>
        /// <returns>Объект, НЕ содержащий null поля</returns>
        protected virtual T RepairNullFields(T data)
        {
            return new T();
        }

        /// <summary>
        /// Починить json, если возникла ошибка десериализации.
        /// Ошибки возможны при плохой обратной совместимости объектов данных после их изменения в новых версиях.
        /// Текущая реализация возвращает пустой json объект.
        /// </summary>
        /// <param name="jsonData">json, который необходимо починить</param>
        /// <returns>корректный json</returns>
        protected virtual string RepairJson(string jsonData) => "{}";

        private T TryToRepairNullFields(T data)
        {
            if (new Validator().HasJsonNullValues(SerializeData(data)))
            {
                return RepairNullFields(data);
            }
            else return data;
        }

        private string SerializeData(T data)
        {
            return _jsonConvertor.SerializeObject(data);
        }

        private T DeserializeData(string jsonData)
        {
            try
            {
                return _jsonConvertor.DeserializeObject(jsonData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return RepairJsonAndDeserialize(jsonData);
            }
        }

        private T RepairJsonAndDeserialize(string jsonData)
        {
            Debug.LogWarning($"Start repairing json data:\n{jsonData}");

            string repairedJson = RepairJson(jsonData);

            try
            {
                return _jsonConvertor.DeserializeObject(repairedJson);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return new T();
            }
        }
    }
}
