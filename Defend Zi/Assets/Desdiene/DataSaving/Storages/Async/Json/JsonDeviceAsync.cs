using System;
using Assets.Desdiene.Tools;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Сохранить файл в json формате на устройство.
    /// 
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
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

        protected override void ReadJson(Action<bool, string> result)
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

        protected sealed override void UpdateJson(string jsonData, Action<bool> successResult)
        {
            // try-catch исключений происходит в родительском классе.
            DeviceFile.WriteAllText(_filePath, jsonData);
            successResult?.Invoke(true);
        }

        protected sealed override void DeleteData(Action<bool> successResult)
        {
            throw new NotImplementedException();
        }
    }
}
