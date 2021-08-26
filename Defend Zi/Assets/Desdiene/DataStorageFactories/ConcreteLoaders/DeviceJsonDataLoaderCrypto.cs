using System;
using Desdiene.DataStorageFactories.Data;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.DataStorageFactories.Encryption;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.DataStorageFactories.ConcreteLoaders
{
    public class DeviceJsonDataLoaderCrypto<T> : DeviceJsonDataLoader<T>, IStorageDataLoader<T> where T : IData, new()
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

        protected override void ReadFromStorage(Action<string> jsonDataCallback)
        {
            LoadAndDecryptData(jsonDataCallback.Invoke);
        }

        protected override void WriteToStorage(string jsonData)
        {
            string modifiedData = jsonEncryption.Encrypt(jsonData);
            base.WriteToStorage(modifiedData);
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
