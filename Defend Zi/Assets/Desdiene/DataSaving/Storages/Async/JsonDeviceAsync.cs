using System;
using System.IO;
using Assets.Desdiene.Tools;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Загрузить/Сохранить/Удалить файл в json формате на устройство.
    /// 
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
    public sealed class JsonDeviceAsync<T> : JsonStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        private readonly string _filePath;
        private readonly DeviceDataReader _deviceDataReader;

        public JsonDeviceAsync(MonoBehaviourExt mono, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base("Асинхронное хранилище Json данных на устройстве",
                  baseFileName,
                  jsonDeserializer)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            _filePath = new FilePath(FileName).Value;
            _deviceDataReader = new DeviceDataReader(mono, _filePath);
        }

        protected sealed override void ReadJson(Action<bool, string> result)
        {
            _deviceDataReader.Read((success, jsonData) =>
            {
                if (!success)
                {
                    result?.Invoke(false, jsonData);
                    return;
                }

                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    result?.Invoke(true, EmptyJson);
                    return;
                }

                result?.Invoke(true, jsonData);
            });
        }

        protected sealed override void UpdateJson(string jsonData, Action<bool> result)
        {
            DeviceFile.WriteAllText(_filePath, jsonData);
            result?.Invoke(true);
        }

        protected sealed override void DeleteData(Action<bool> result)
        {
            File.Delete(_filePath);
            result?.Invoke(true);
        }
    }
}
