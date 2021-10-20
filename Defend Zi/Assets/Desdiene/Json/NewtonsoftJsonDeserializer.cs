using Newtonsoft.Json;
using UnityEngine;

namespace Desdiene.Json
{
    /// <summary>
    /// Данный класс используется для десериализации конкретного объекта.
    /// </summary>
    /// <typeparam name="T">Тип десериализуемого объекта</typeparam>
    public class NewtonsoftJsonDeserializer<T> : IJsonDeserializer<T> where T : new()
    {
        private const string EmptyJson = "{}";
        private readonly JsonSerializerSettings _serializerSettings;

        public NewtonsoftJsonDeserializer() : this(new JsonSerializerSettings()) { }

        public NewtonsoftJsonDeserializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        T IJsonDeserializer<T>.ToObject(string json)
        {
            json = string.IsNullOrWhiteSpace(json)
                ? EmptyJson
                : json;
            T data = JsonConvert.DeserializeObject<T>(json, _serializerSettings);
            Debug.Log($"Десериализация json-а.\nJson:\n{json}\n\nОбъект:\n{data}");
            return data;
        }
    }
}
