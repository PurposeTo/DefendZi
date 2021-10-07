using System;
using Desdiene.Containers;
using Desdiene.DataStorageFactories.Datas;
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
    public abstract class JsonDataLoader<T> : MonoBehaviourExtContainer, IDataLoader<T> where T : IData, new()
    {
        private readonly string _storageName;
        private readonly IJsonConvertor<T> _jsonConvertor;

        /// <param name="storageName">Имя хранилища</param>
        /// <param name="fileName">Имя сохраняемого файла</param>
        /// <param name="fileName">расширение сохраняемого файла</param>
        /// <param name="jsonConvertor">json (де)сериализатор</param>
        public JsonDataLoader(MonoBehaviourExt mono,
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

            _storageName = storageName;
            FileName = fileName;
            _jsonConvertor = jsonConvertor ?? throw new ArgumentNullException(nameof(jsonConvertor));
        }

        string IDataLoader<T>.StorageName => _storageName;
        protected string FileName { get; }
        protected string FileExtension => "json";
        protected string FileNameWithExtension => FileName + "." + FileExtension;

        /// <summary>
        /// Возвращает коллбеком данные из хранилища.
        /// Не вызовется, если произошли проблемы при чтении.
        /// </summary>
        /// <param name="dataCallback"></param>
        void IDataLoader<T>.Load(Action<T> dataCallback)
        {
            Debug.Log($"Начата загрузка данных с [{((IDataLoader<T>)this).StorageName}]");
            ReadFromStorage(jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.Log($"Данные на [{((IDataLoader<T>)this).StorageName}] не найдены");
                    dataCallback?.Invoke(new T());
                }
                else
                {
                    Debug.Log($"Данные с [{((IDataLoader<T>)this).StorageName}] загружены\nДанные:\n{jsonData}");
                    T data = DeserializeData(jsonData);
                    data.TryToRepair();
                    dataCallback?.Invoke(data);
                }
            });
        }

        void IDataLoader<T>.Save(T data)
        {
            Debug.Log($"Начато сохранение данных на [{((IDataLoader<T>)this).StorageName}]");
            if (data.IsValid())
            {
                string jsonData = SerializeData(data);
                WriteToStorage(jsonData);
            }
            else Debug.LogError($"Data is not valid!\n{data}");
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
            Debug.LogWarning($"Repaired json data:\n{repairedJson}");

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
