using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract class JsonStorage<T> : FileStorage<T> where T : IJsonSerializable, IValidData
    {
        protected const string JsonFileExtension = "json";
        protected const string EmptyJson = "{}";
        private readonly IJsonDeserializer<T> _jsonDeserializer;

        protected JsonStorage(string storageName, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base(storageName, baseFileName, JsonFileExtension)
        {
            _jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(jsonDeserializer));
        }

        protected abstract bool TryToReadJson(out string data);
        protected abstract bool UpdateJson(string jsonData);

        protected sealed override bool TryToReadData(out T data)
        {
            if (TryToReadJson(out string json))
            {
                data = Deserialize(json);
                return true;
            }
            else
            {
                data = default;
                return false;
            }
        }

        protected sealed override bool UpdateData(T data)
        {
            string jsonData = Serialize(data);
            return UpdateJson(jsonData);
        }

        /// <summary>
        /// Cериализовать объект в json.
        /// Исключение при сериализации данных считать как неудачная запись данных.
        /// </summary>
        private string Serialize(T data) => data.ToJson();

        /// <summary>
        /// Десериализовать json в объект.
        /// Исключение при десериализации НЕ считать как неудачное считывание данных.
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
