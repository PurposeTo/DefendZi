﻿using Newtonsoft.Json;
using UnityEngine;

namespace Desdiene.JsonConvertorWrapper
{
    /// <summary>
    /// Данный класс используется для хранения serializerSettings.
    /// Имеет методы (де)сериализации json-а.
    /// Обращаться через интерфейс.
    /// </summary>
    /// <typeparam name="T">Тип (де)сериализуемого объекта</typeparam>
    public class NewtonsoftJsonConvertor<T> : IJsonConvertor<T> where T : new()
    {
        private readonly JsonSerializerSettings serializerSettings;

        public NewtonsoftJsonConvertor() : this(new JsonSerializerSettings()) { }

        public NewtonsoftJsonConvertor(JsonSerializerSettings serializerSettings)
        {
            this.serializerSettings = serializerSettings;
        }

        T IJsonConvertor<T>.DeserializeObject(string jsonData)
        {
            T data = JsonConvert.DeserializeObject<T>(jsonData, serializerSettings);
            Debug.Log("Десериализованные данные:\n" + data);
            return data;
        }

        string IJsonConvertor<T>.SerializeObject(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data, serializerSettings);
            Debug.Log("Сериализованные данные:\n" + jsonData);
            return jsonData;
        }
    }
}