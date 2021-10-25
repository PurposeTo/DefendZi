using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract class JsonStorageAsync<T> : FileStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        private const string JsonFileExtension = "json";
        protected const string EmptyJson = "{}";
        private readonly IJsonDeserializer<T> _jsonDeserializer;

        protected JsonStorageAsync(string storageName, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base(storageName, baseFileName, JsonFileExtension)
        {
            _jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(jsonDeserializer));
        }

        protected abstract void ReadJson(Action<bool, string> result);
        protected abstract void UpdateJson(string jsonData, Action<bool> successResult);

        protected sealed override void ReadData(Action<bool, T> result)
        {
            ReadJson((success, jsonData) =>
            {
                T data = success
                ? Deserialize(jsonData)
                : default;

                result?.Invoke(success, data);
            });
        }

        protected sealed override void UpdateData(T data, Action<bool> successResult)
        {
            string jsonData = Serialize(data);
            UpdateJson(jsonData, successResult);
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
