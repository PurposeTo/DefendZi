﻿using System;
using System.IO;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Json
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceJsonDataLoader<T> : JsonDataLoader<T>, IDataLoader<T> where T : IData, new()
    {
        protected readonly string _filePath;
        protected readonly DeviceDataLoader _deviceDataLoader;

        public DeviceJsonDataLoader(MonoBehaviourExt mono,
                                    string storageName,
                                    string fileName,
                                    IJsonConvertor<T> jsonConvertor)
            : base(mono, storageName, fileName, jsonConvertor)
        {
            _filePath = FilePathGetter.GetFilePath(FileNameWithExtension);
            Debug.Log($"{((IDataLoader<T>)this).StorageName}. Путь к файлу данных : {_filePath}");
            _deviceDataLoader = new DeviceDataLoader(MonoBehaviourExt, _filePath);
        }

        public DeviceJsonDataLoader(MonoBehaviourExt mono, string fileName, IJsonConvertor<T> jsonConvertor)
            : this(mono, "Локальное хранилище", fileName, jsonConvertor)
        { }

        protected override void LoadJsonData(Action<string> jsonDataCallback)
        {
            _deviceDataLoader.ReadDataFromDevice(jsonDataCallback.Invoke);
        }

        protected override void SaveJsonData(string jsonData, Action<bool> successCallback)
        {
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                throw new ArgumentException($"{nameof(jsonData)} не может быть пустым или иметь значение null");
            }

            // TODO: А если у пользователя недостаточно памяти, чтобы создать файл?

            bool success = false;
            try
            {
                File.WriteAllText(_filePath, jsonData);
                success = true;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }

            successCallback?.Invoke(success);
        }
    }
}