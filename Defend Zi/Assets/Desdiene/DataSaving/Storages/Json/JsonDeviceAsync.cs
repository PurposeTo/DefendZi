using System;
using Assets.Desdiene.Tools;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDeviceAsync<T> : JsonStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        protected readonly string _filePath;
        protected readonly DeviceDataReader _deviceDataReader;

        public JsonDeviceAsync(MonoBehaviourExt mono, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base("Асинхронное хранилище Json данных на устройстве",
                  baseFileName,
                  jsonDeserializer)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            _filePath = new FilePath(FileName).Value;
            _deviceDataReader = new DeviceDataReader(mono, _filePath);
        }

        protected override void LoadJson(Action<bool, string> result)
        {
            _deviceDataReader.Read((success, jsonData) =>
            {
                if(success && string.IsNullOrWhiteSpace(jsonData))
                {
                    jsonData = EmptyJson;
                }

                result?.Invoke(success, jsonData);
            });
        }

        protected sealed override void SaveJson(string jsonData, Action<bool> successResult)
        {
            // try-catch исключений происходит в родительском классе.
            LocalFile.WriteAllText(_filePath, jsonData);
            successResult?.Invoke(true);
        }

        protected sealed override void Clean(Action<bool> successResult)
        {
            throw new NotImplementedException();
        }
    }
}
