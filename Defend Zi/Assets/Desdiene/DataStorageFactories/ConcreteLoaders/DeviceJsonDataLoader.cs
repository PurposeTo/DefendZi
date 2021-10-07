using System;
using System.IO;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.DataStorageFactories.DataLoaders.FromStorage;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.DataStorageFactories.ConcreteLoaders
{
    /// <summary>
    /// Данный класс занимается загрузкой, сохранением и валидацией json данных с устройства.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceJsonDataLoader<T> : StorageJsonDataLoader<T>, IStorageDataLoader<T> where T : IData, new()
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
            Debug.Log($"{StorageName}. Путь к файлу данных : {_filePath}");
            _deviceDataLoader = new DeviceDataLoader(MonoBehaviourExt, _filePath);
        }

        public DeviceJsonDataLoader(MonoBehaviourExt mono, string fileName, IJsonConvertor<T> jsonConvertor)
            : this(mono, "Локальное хранилище", fileName, jsonConvertor)
        { }

        protected override void ReadFromStorage(Action<string> jsonDataCallback)
        {
            _deviceDataLoader.ReadDataFromDevice(jsonDataCallback.Invoke);
        }

        protected override void WriteToStorage(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentException($"{nameof(jsonData)} не может быть пустым или иметь значение null");
            }

            // TODO: А если у пользователя недостаточно памяти, чтобы создать файл?

            File.WriteAllText(_filePath, jsonData);
        }
    }
}
