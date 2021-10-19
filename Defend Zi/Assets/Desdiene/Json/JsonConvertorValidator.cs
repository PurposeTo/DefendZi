using System;
using Desdiene.DataStorageFactories.Datas;
using UnityEngine;

namespace Desdiene.Json
{
    public class JsonConvertorValidator<T> : IJsonConvertor<T> where T : IData, new()
    {
        private readonly IJsonConvertor<T> _jsonConvertor;

        public JsonConvertorValidator(IJsonConvertor<T> jsonConvertor)
        {
            _jsonConvertor = jsonConvertor ?? throw new ArgumentNullException(nameof(jsonConvertor));
        }

        string IJsonConvertor<T>.Serialize(T data)
        {
            data.TryToRepair();
            return _jsonConvertor.Serialize(data);
        }

        T IJsonConvertor<T>.Deserialize(string jsonData)
        {
            try
            {
                T data = _jsonConvertor.Deserialize(jsonData);
                data.TryToRepair();
                return data;
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
                return new T();
            }
        }
    }
}
