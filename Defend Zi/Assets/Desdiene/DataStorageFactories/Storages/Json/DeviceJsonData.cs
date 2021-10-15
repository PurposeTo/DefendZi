using System;
using System.IO;
using Assets.Desdiene.Tools;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages.Json
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceJsonData<T> : StorageJsonData<T>, IStorageData<T> where T : IData, new()
    {
        protected readonly string _filePath;
        protected readonly DeviceDataLoader _deviceDataLoader;

        public DeviceJsonData(MonoBehaviourExt mono,
                                    string storageName,
                                    string fileName,
                                    IJsonConvertor<T> jsonConvertor)
            : base(mono, storageName, fileName, jsonConvertor)
        {
            _filePath = new FilePath(FileNameWithExtension).Get();
            Debug.Log($"{((IStorageData<T>)this).StorageName}. Путь к файлу данных : {_filePath}");
            _deviceDataLoader = new DeviceDataLoader(MonoBehaviourExt, _filePath);
        }

        public DeviceJsonData(MonoBehaviourExt mono, string fileName, IJsonConvertor<T> jsonConvertor)
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
                LocalFile.WriteAllText(_filePath, jsonData);
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
