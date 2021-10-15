using System;
using System.IO;
using UnityEngine;

namespace Desdiene.Tools
{
    public class FilePath
    {
        private readonly string _fileNameWithExtension;

        public FilePath(string fileNameWithExtension)
        {
            _fileNameWithExtension = fileNameWithExtension;
        }

        public string Get()
        {
            var runningPlatform = Application.platform;

            switch (runningPlatform)
            {
                case RuntimePlatform.WindowsEditor:
                    return Path.Combine(Application.dataPath, _fileNameWithExtension);
                case RuntimePlatform.Android:
                    string androidPathPrefix = "file://";
                    return androidPathPrefix + Path.Combine(Application.persistentDataPath, _fileNameWithExtension);
                default:
                    Debug.LogError($"{runningPlatform} is unknown platform to PathToFile.Get()!");
                    return "";
            }
        }
    }
}
