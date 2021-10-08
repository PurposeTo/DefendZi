using System;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.Encryptions;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.DataStorageFactories.DataLoaders.Json
{
    public class DeviceJsonDataLoaderCrypto<T> : DeviceJsonDataLoader<T>, IDataLoader<T> where T : IData, new()
    {
        private readonly JsonEncryption jsonEncryption;

        public DeviceJsonDataLoaderCrypto(MonoBehaviourExt superMono,
                                          string fileName,
                                          IJsonConvertor<T> jsonConvertor)
            : base(superMono,
                   "Локальное зашифрованное хранилище",
                   fileName,
                   jsonConvertor)
        {
            jsonEncryption = new JsonEncryption(FileName, FileExtension);
        }

        protected override void LoadJsonData(Action<string> jsonDataCallback)
        {
            LoadAndDecryptData(jsonDataCallback.Invoke);
        }

        protected override void SaveJsonData(string jsonData, Action<bool> successCallback)
        {
            string modifiedData = jsonEncryption.Encrypt(jsonData);
            base.SaveJsonData(modifiedData, successCallback);
        }

        private void LoadAndDecryptData(Action<string> jsonDataCallback)
        {
            _deviceDataLoader.ReadDataFromDevice(receivedData =>
            {
                jsonDataCallback?.Invoke(jsonEncryption.Decrypt(receivedData));
            });
        }
    }
}
