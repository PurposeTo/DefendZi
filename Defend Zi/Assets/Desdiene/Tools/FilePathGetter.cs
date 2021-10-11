using System.IO;
using UnityEngine;

namespace Desdiene.Tools
{
    public static class FilePathGetter
    {
        private static readonly string _androidPathPrefix = "file://";

        public static string GetFilePath(string fileName)
        {
            var runningPlatform = Application.platform;

            switch (runningPlatform)
            {
                case RuntimePlatform.WindowsEditor:
                    return Path.Combine(Application.dataPath, fileName);
                case RuntimePlatform.Android:
                    return _androidPathPrefix + Path.Combine(Application.persistentDataPath, fileName);
                default:
                    Debug.LogError($"{runningPlatform} is unknown platform to GetFilePath()!");
                    return "";
            }
        }
    }
}
