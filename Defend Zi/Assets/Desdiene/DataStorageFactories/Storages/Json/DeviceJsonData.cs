using System;
using System.IO;
using Assets.Desdiene.Tools;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages.Json
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceJsonData<T> : StorageJsonData<T>, IDataStorageOld<T> where T : IData, new()
    {
        protected readonly string _filePath;
        protected readonly DeviceDataReader _deviceDataLoader;

        public DeviceJsonData(MonoBehaviourExt mono,
                                    string storageName,
                                    string fileName,
                                    IJsonConvertor<T> jsonConvertor)
            : base(mono, storageName, fileName, jsonConvertor)
        {
            _filePath = new FilePath(FileNameWithExtension).Value;
            Debug.Log($"{storageName}. Путь к файлу данных : {_filePath}");
            _deviceDataLoader = new DeviceDataReader(MonoBehaviourExt, _filePath);
        }

        public DeviceJsonData(MonoBehaviourExt mono, string fileName, IJsonConvertor<T> jsonConvertor)
            : this(mono, "Локальное хранилище", fileName, jsonConvertor)
        { }

        protected override void LoadJsonData(Action<string> jsonDataCallback)
        {
            _deviceDataLoader.Read(jsonDataCallback.Invoke);
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
                DeviceFile.WriteAllText(_filePath, jsonData);
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
