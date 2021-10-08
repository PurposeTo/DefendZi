using System;
using System.Collections;
using System.IO;
using Desdiene.Containers;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.Networking;

namespace Desdiene.Tools
{
    public class DeviceDataLoader : MonoBehaviourExtContainer
    {
        private readonly string filePath;
        private readonly int retryCount = 3;

        private readonly ICoroutine _dataReading;

        public DeviceDataLoader(MonoBehaviourExt superMonoBehaviour, string filePath) : base(superMonoBehaviour)
        {
            this.filePath = filePath;
            _dataReading = new CoroutineWrap(superMonoBehaviour);
        }

        /// <summary>
        /// Загрузить данные с устройства.
        /// </summary>
        /// <param name="jsonAction">Полученные данные. Может быть null, если данные не были найдены.</param>
        /// <returns></returns>
        public void ReadDataFromDevice(Action<string> stringDataCallback)
        {
            _dataReading.StartContinuously(ReadDataEnumerator(stringDataCallback.Invoke));
        }

        private IEnumerator ReadDataEnumerator(Action<string> stringDataCallback)
        {
            var platform = Application.platform;

            switch (platform)
            {
                case RuntimePlatform.Android:
                    yield return _dataReading.StartNested(LoadViaAndroid(stringDataCallback));
                    break;
                case RuntimePlatform.WindowsEditor:
                    stringDataCallback?.Invoke(LoadViaEditor());
                    break;
                default:
                    Debug.LogError($"{platform} is unknown platform!");
                    yield return _dataReading.StartNested(LoadViaAndroid(stringDataCallback));
                    break;
            }
        }

        private string LoadViaEditor()
        {
            return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
        }

        private IEnumerator LoadViaAndroid(Action<string> stringDataCallback)
        {
            string data = null;

            using (UnityWebRequest request = new UnityWebRequest { url = filePath, downloadHandler = new DownloadHandlerBuffer() })
            {
                bool dataWasLoaded = false;

                for (int i = 0; !dataWasLoaded || i < retryCount; i++)
                {
                    yield return request.SendWebRequest();

                    if (request.error != null || request.responseCode == 404)
                    {
                        Debug.LogWarning($"Сaught an exception while loading data from android:\n{request.error}");
                        yield return null;
                    }
                    else
                    {
                        data = request.downloadHandler.text;
                        dataWasLoaded = true;
                    }
                }
            }

            stringDataCallback?.Invoke(data);
        }
    }
}
