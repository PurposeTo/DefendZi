﻿using System;
using System.IO;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.GameDataAsset.ConcreteLoaders
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceJsonDataLoader<T> :
        StorageJsonDataLoader<T>,
        IStorageDataLoader<T>
        where T : GameData, new()
    {
        protected readonly string filePath;
        protected readonly DeviceDataLoader deviceDataLoader;

        public DeviceJsonDataLoader(MonoBehaviourExt superMono,
                                    string storageName,
                                    string fileName,
                                    IJsonConvertor<T> jsonConvertor)
            : base(superMono, storageName, fileName, jsonConvertor)
        {
            filePath = FilePathGetter.GetFilePath(FileNameWithExtension);
            Debug.Log($"{StorageName}. Путь к файлу данных : {filePath}");
            deviceDataLoader = new DeviceDataLoader(monoBehaviourExt, filePath);
        }

        public DeviceJsonDataLoader(MonoBehaviourExt superMono, string fileName, IJsonConvertor<T> jsonConvertor)
            : this(superMono, "Локальное хранилище", fileName, jsonConvertor)
        { }

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
