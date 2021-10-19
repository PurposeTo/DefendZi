using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

namespace Desdiene.DataSaving.Storages
{
    public abstract class JsonStorage<T> : Storage<T> where T : IJsonSerializable, IValidData
    {
        protected const string FileExtension = "json";
        protected const string EmptyJson = "{}";
        private readonly IJsonDeserializer<T> _jsonDeserializer;

        protected JsonStorage(string storageName, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
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

        protected sealed override T Load()
        {
            string jsonData = LoadJson();
            return _jsonDeserializer.ToObject(jsonData);
        }

        protected sealed override bool Save(T data)
        {
            string jsonData = data.ToJson();
            return SaveJson(jsonData);
        }

        protected abstract string LoadJson();
        protected abstract bool SaveJson(string jsonData);
    }
}
