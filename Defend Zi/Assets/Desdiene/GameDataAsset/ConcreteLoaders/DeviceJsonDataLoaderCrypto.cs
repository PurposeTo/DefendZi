using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.GameDataAsset.Encryption;
using Desdiene.JsonConvertorWrapper;
using Desdiene.SuperMonoBehaviourAsset;

namespace Desdiene.GameDataAsset.ConcreteLoaders
{
    public class DeviceJsonDataLoaderCrypto<T> :
        DeviceJsonDataLoader<T>,
        IStorageDataLoader<T>
         where T : GameData, new()
    {
        private readonly JsonEncryption jsonEncryption;

        public DeviceJsonDataLoaderCrypto(SuperMonoBehaviour superMono,
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
            deviceDataLoader.ReadDataFromDevice(receivedData =>
            {
                jsonDataCallback?.Invoke(jsonEncryption.Decrypt(receivedData));
            });
        }
    }
}
