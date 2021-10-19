using System;
using System.IO;
using UnityEngine;

namespace Desdiene.Tools
{
    public class FilePath
    {
        public FilePath(string fileNameWithExtension)
        {
            if (string.IsNullOrWhiteSpace(fileNameWithExtension))
            {
                throw new ArgumentException($"{nameof(fileNameWithExtension)} can't be null or empty");
            }

            // TODO: проверить параметр по regex: {символы, точка, символы}

            Value = Init(fileNameWithExtension);
            Debug.Log($"Got filePath: {Value}");
        }

        public string Value { get; }

        private string Init(string fileNameWithExtension)
        {
            var runningPlatform = Application.platform;

            switch (runningPlatform)
            {
                case RuntimePlatform.WindowsEditor:
                    return Path.Combine(Application.dataPath, fileNameWithExtension);
                case RuntimePlatform.Android:
                    string androidPathPrefix = "file://";
                    return androidPathPrefix + Path.Combine(Application.persistentDataPath, fileNameWithExtension);
                default:
                    Debug.LogError($"{runningPlatform} is unknown platform to PathToFile.Get()!");
                    return fileNameWithExtension;
            }
        }
    }
}
