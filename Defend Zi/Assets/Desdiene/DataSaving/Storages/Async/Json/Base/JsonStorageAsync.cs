using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract class JsonStorageAsync<T> : StorageAsync<T> where T : IJsonSerializable, IValidData
    {
        protected const string FileExtension = "json";
        protected const string EmptyJson = "{}";
        private readonly IJsonDeserializer<T> _jsonDeserializer;

        protected JsonStorageAsync(string storageName, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base(storageName)
        {
            if (string.IsNullOrWhiteSpace(baseFileName))
            {
                throw new ArgumentException($"{nameof(baseFileName)} can't be null or empty");
            }

            _jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(jsonDeserializer));
            BaseFileName = baseFileName;
        }

        protected string BaseFileName { get; }
        protected string FileName => BaseFileName + "." + FileExtension;

        protected abstract void LoadJson(Action<bool, string> result);
        protected abstract void SaveJson(string jsonData, Action<bool> successResult);

        protected sealed override void Load(Action<bool, T> result)
        {
            LoadJson((success, jsonData) =>
            {
                T data = success
                ? Deserialize(jsonData)
                : default;

                result?.Invoke(success, data);
            });
        }

        protected sealed override void Save(T data, Action<bool> successResult)
        {
            string jsonData = Serialize(data);
            SaveJson(jsonData, successResult);
        }

        /// <summary>
        /// Cериализовать объект в json.
        /// Исключение при сериализации данных считать как неудачная запись данных.
        /// </summary>
        private string Serialize(T data) => data.ToJson();

        /// <summary>
        /// Десериализовать json в объект.
        /// исключение при десериализации НЕ считать как неудачное считывание данных
        /// </summary>
        private T Deserialize(string json)
        {
            try
            {
                return _jsonDeserializer.ToObject(json);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Deserialization exception! Json:\n{json}\n\n{exception}");
                return _jsonDeserializer.ToObject(EmptyJson);
            }
        }
    }
}
