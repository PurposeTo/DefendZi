using System;
using Assets.Desdiene.Tools;
using Desdiene.DataSaving.Datas;
using Desdiene.Encryptions;
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
    public sealed class JsonCryptoDeviceAsync<T> : JsonStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        private readonly string _filePath;
        private readonly DeviceDataReader _deviceDataReader;
        private readonly SavingStringEncryptor _encryptor;

        public JsonCryptoDeviceAsync(MonoBehaviourExt mono, string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base("Асинхронное хранилище зашифрованных Json данных на устройстве",
                  baseFileName,
                  jsonDeserializer)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            _filePath = new FilePath(FileName).Value;
            _deviceDataReader = new DeviceDataReader(mono, _filePath);
            _encryptor = new SavingStringEncryptor(mono, FileName);
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

                _encryptor.Decrypt(jsonData, decryptedData =>
                {
                    result?.Invoke(true, decryptedData);
                });
            });
        }

        protected sealed override void UpdateJson(string jsonData, Action<bool> result)
        {
            string modifiedData = _encryptor.Encrypt(jsonData);
            // try-catch исключений происходит в родительском классе.
            DeviceFile.WriteAllText(_filePath, modifiedData);
            result?.Invoke(true);
        }

        protected sealed override void DeleteData(Action<bool> result)
        {
            throw new NotImplementedException();
        }
    }
}
