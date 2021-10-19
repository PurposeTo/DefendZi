using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Сохранить файл в json формате в PlayerPrefs.
    /// 
    /// Запись данных из PlayerPrefs на диск происходит во время выхода из приложения:
    /// https://docs.unity3d.com/ScriptReference/PlayerPrefs.Save.html
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
    public sealed class PlayerPrefsJsonStorage<T> : JsonStorage<T> where T : IJsonSerializable, IValidData
    {
        public PlayerPrefsJsonStorage(string baseFileName, IJsonDeserializer<T> jsonDeserializer)
            : base("PlayerPrefs storage",
                  baseFileName,
                  jsonDeserializer)
        { }

        protected sealed override string LoadJson()
        {
            return PlayerPrefs.HasKey(FileName)
                ? PlayerPrefs.GetString(FileName)
                : EmptyJson;
        }

        protected sealed override bool SaveJson(string jsonData)
        {
            PlayerPrefs.SetString(FileName, jsonData);
            throw new NotImplementedException();
        }

        protected sealed override bool TryToClean()
        {
            PlayerPrefs.DeleteKey(FileName);
            return !PlayerPrefs.HasKey(FileName);
        }
    }
}
