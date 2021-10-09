using Desdiene.DataStorageFactories.Datas;
using Newtonsoft.Json;
using UnityEngine;

namespace Desdiene.JsonConvertorWrapper
{
    /// <summary>
    /// Данный класс используется для хранения serializerSettings.
    /// Имеет методы (де)сериализации json-а.
    /// Обращаться через интерфейс.
    /// </summary>
    /// <typeparam name="T">Тип (де)сериализуемого объекта</typeparam>
    public class NewtonsoftJsonConvertor<T> : IJsonConvertor<T> where T : IData, new()
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public NewtonsoftJsonConvertor() : this(new JsonSerializerSettings()) { }

        public NewtonsoftJsonConvertor(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        T IJsonConvertor<T>.Deserialize(string jsonData)
        {
            T data = JsonConvert.DeserializeObject<T>(jsonData, _serializerSettings);
            Debug.Log("Десериализованные данные:\n" + data);
            return data;
        }

        string IJsonConvertor<T>.Serialize(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data, _serializerSettings);
            Debug.Log("Сериализованные данные:\n" + jsonData);
            return jsonData;
        }
    }
}
