using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.JsonConvertorWrapper;
using Desdiene.SuperMonoBehaviourAsset;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Storage
{
    public abstract class JsonDataLoader<T> : ReaderWriter<T> where T : GameData, new()
    {
        public string Name { get; } // Имя конкретного хранилища

        protected string FileName { get; }
        protected string FileExtension => "json";
        protected string FileNameWithExtension => FileName + "." + FileExtension;

        private readonly Validator validator = new Validator();
        private readonly IJsonConvertor<T> jsonConvertor;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="superMonoBehaviour"></param>
        /// <param name="storageName">Имя хранилища</param>
        /// <param name="fileName">Имя сохраняемого файла</param>
        /// <param name="fileName">расширение сохраняемого файла</param>
        /// <param name="jsonConvertor">json (де)сериализатор</param>
        public JsonDataLoader(SuperMonoBehaviour superMonoBehaviour,
            string storageName,
            string fileName,
            IJsonConvertor<T> jsonConvertor)
            : base(superMonoBehaviour)
        {
            if (superMonoBehaviour is null) throw new ArgumentNullException(nameof(superMonoBehaviour));

            if (string.IsNullOrEmpty(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} не может быть пустым или иметь значение null");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} не может быть пустым или иметь значение null");
            }

            Name = storageName;
            FileName = FileName;
            this.jsonConvertor = jsonConvertor ?? throw new ArgumentNullException(nameof(jsonConvertor));
        }

        /// <summary>
        /// Возвращает коллбеком данные из хранилища.
        /// Не вызовется, если произошли проблемы при чтении.
        /// </summary>
        /// <param name="dataCallback"></param>
        public override void Load(Action<T> dataCallback)
        {
            ReadFromStorage(jsonData =>
            {
                if (string.IsNullOrEmpty(jsonData)) dataCallback?.Invoke(new T());
                else
                {
                    T data = DeserializeData(jsonData);
                    data = TryToRepairNullFields(data);
                    dataCallback?.Invoke(data);
                }
            });
        }

        public override void Save(T data)
        {
            string jsonData = SerializeData(data);
            if (validator.HasJsonNullValues(jsonData)) return;
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
            return jsonConvertor.SerializeObject(data);
        }

        private T DeserializeData(string jsonData)
        {
            try
            {
                return jsonConvertor.DeserializeObject(jsonData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return RepairJsonAndDeserialize(jsonData);
            }
        }

        private T RepairJsonAndDeserialize(string jsonData)
        {
            Debug.Log($"Start repairing json data:\n{jsonData}");

            string repairedJson = RepairJson(jsonData);

            try
            {
                return jsonConvertor.DeserializeObject(repairedJson);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return new T();
            }
        }
    }
}
