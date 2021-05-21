using System;
using System.IO;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.JsonConvertorWrapper;
using Desdiene.SuperMonoBehaviourAsset;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.GameDataAsset.ConcreteLoaders
{
    public class JsonLocalStorage<T> : JsonDataLoader<T>
         where T : GameData, new()
    {
        protected readonly string filePath;
        protected readonly DeviceDataLoader deviceDataLoader;

        public JsonLocalStorage(SuperMonoBehaviour superMonoBehaviour, 
            string fileName, 
            IJsonConvertor<T> jsonConvertor)
            : base(superMonoBehaviour, 
                  "Локальное хранилище", 
                  fileName,
                  jsonConvertor)
        {
            filePath = FilePathGetter.GetFilePath(FileNameWithExtension);
            Debug.Log($"{Name}. Путь к файлу данных : {filePath}");
            deviceDataLoader = new DeviceDataLoader(superMonoBehaviour, filePath);
        }

        protected override void ReadFromStorage(Action<string> jsonDataCallback)
        {
            deviceDataLoader.ReadDataFromDevice(jsonDataCallback.Invoke);
        }

        protected override void WriteToStorage(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentException($"{nameof(jsonData)} не может быть пустым или иметь значение null");
            }

            // TODO: А если у пользователя недостаточно памяти, чтобы создать файл?

            File.WriteAllText(filePath, jsonData);
        }
    }
}
