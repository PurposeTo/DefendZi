﻿using System;
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

        private bool DataExists => PlayerPrefs.HasKey(FileName);

        protected sealed override string LoadJson()
        {
            return DataExists
                ? PlayerPrefs.GetString(FileName)
                : EmptyJson;
        }

        protected sealed override bool SaveJson(string jsonData)
        {
            PlayerPrefs.SetString(FileName, jsonData);
            return DataExists;
        }

        protected sealed override bool TryClean()
        {
            PlayerPrefs.DeleteKey(FileName);
            return !DataExists;
        }
    }
}
