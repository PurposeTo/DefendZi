using Newtonsoft.Json;
using UnityEngine;

namespace Desdiene.Json
{
    /// <summary>
    /// Данный класс используется для хранения serializerSettings.
    /// Имеет методы (де)сериализации json-а.
    /// Обращаться через интерфейс.
    /// </summary>
    /// <typeparam name="T">Тип (де)сериализуемого объекта</typeparam>
    public class NewtonsoftJsonConvertor<T> : IJsonConvertor<T> where T : new()
    {
        private const string EmptyJson = "{}";
        private readonly JsonSerializerSettings _serializerSettings;

        public NewtonsoftJsonConvertor() : this(new JsonSerializerSettings()) { }

        public NewtonsoftJsonConvertor(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        string IJsonSerializer<T>.ToJson(T data)
        {
            string json = JsonConvert.SerializeObject(data, _serializerSettings);
            Debug.Log($"Cериализация json-а.\nОбъект:\n{data}\n\nJson:\n{json}");
            return json;
        }

        T IJsonDeserializer<T>.ToObject(string json)
        {
            json = RepairJson(json);
            T data = JsonConvert.DeserializeObject<T>(json, _serializerSettings);
            Debug.Log($"Десериализация json-а.\nJson:\n{json}\n\nОбъект:\n{data}");
            return data;
        }

        private string RepairJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return EmptyJson;
            }

            return json;
        }
    }
}
