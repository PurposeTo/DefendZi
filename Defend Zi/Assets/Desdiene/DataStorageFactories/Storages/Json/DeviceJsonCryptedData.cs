using System;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.Encryptions;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.DataStorageFactories.Storages.Json
{
    public class DeviceJsonCryptedData<T> : DeviceJsonData<T>, IStorageData<T> where T : IData, new()
    {
        private readonly JsonEncryption _jsonEncryption;

        public DeviceJsonCryptedData(MonoBehaviourExt superMono,
                                          string fileName,
                                          IJsonConvertor<T> jsonConvertor)
            : base(superMono,
                   "Локальное зашифрованное хранилище",
                   fileName,
                   jsonConvertor)
        {
            _jsonEncryption = new JsonEncryption(FileName, FileExtension);
        }

        protected override void LoadJsonData(Action<string> jsonDataCallback)
        {
            LoadAndDecryptData(jsonDataCallback.Invoke);
        }

        protected override void SaveJsonData(string jsonData, Action<bool> successCallback)
        {
            string modifiedData = _jsonEncryption.Encrypt(jsonData);
            base.SaveJsonData(modifiedData, successCallback);
        }

        private void LoadAndDecryptData(Action<string> jsonDataCallback)
        {
            _deviceDataLoader.ReadDataFromDevice(receivedData =>
            {
                jsonDataCallback?.Invoke(_jsonEncryption.Decrypt(receivedData));
            });
        }
    }
}
