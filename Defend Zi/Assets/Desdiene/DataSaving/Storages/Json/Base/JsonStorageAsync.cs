using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

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

        protected sealed override void Load(Action<bool, T> result)
        {
            LoadJson((success, jsonData) =>
            {
                T data = success
                ? _jsonDeserializer.ToObject(jsonData)
                : default;

                result?.Invoke(success, data);
            });
        }

        protected sealed override void Save(T data, Action<bool> successResult)
        {
            string jsonData = data.ToJson();
            SaveJson(jsonData, successResult);
        }

        protected abstract void LoadJson(Action<bool, string> result);
        protected abstract void SaveJson(string jsonData, Action<bool> successResult);
    }
}
