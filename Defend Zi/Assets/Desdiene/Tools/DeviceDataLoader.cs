using System;
using System.Collections;
using System.IO;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.Networking;

namespace Desdiene.Tools
{
    public class DeviceDataLoader : MonoBehaviourExtContainer
    {
        private readonly string filePath;
        private readonly int retryCount = 3;

        private readonly ICoroutine loadDataRoutine;

        public DeviceDataLoader(MonoBehaviourExt superMonoBehaviour, string filePath) : base(superMonoBehaviour)
        {
            this.filePath = filePath;
            loadDataRoutine = new CoroutineWrap(superMonoBehaviour);
        }

        /// <summary>
        /// Загрузить данные с устройства.
        /// </summary>
        /// <param name="jsonAction">Полученные данные. Может быть null, если данные не были найдены.</param>
        /// <returns></returns>
        public void ReadDataFromDevice(Action<string> stringDataCallback)
        {
            loadDataRoutine.StartContinuously(ReadDataEnumerator(stringDataCallback.Invoke));
        }


        private IEnumerator ReadDataEnumerator(Action<string> stringDataCallback)
        {
            var platform = Application.platform;

            switch (platform)
            {
                case RuntimePlatform.Android:
                    yield return LoadViaAndroid(stringDataCallback);
                    break;
                case RuntimePlatform.WindowsEditor:
                    stringDataCallback?.Invoke(LoadViaEditor());
                    break;
                default:
                    Debug.LogError($"{platform} is unknown platform!");
                    yield return LoadViaAndroid(stringDataCallback);
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
                for (int i = 0; data == null && i < retryCount; i++)
                {
                    yield return request.SendWebRequest();

                    if (request.error != null || request.responseCode == 404)
                    {
                        Debug.LogWarning(request.error);

                        yield return null;
                    }
                    else data = request.downloadHandler.text;
                }
            }

            stringDataCallback?.Invoke(data);
        }
    }
}
