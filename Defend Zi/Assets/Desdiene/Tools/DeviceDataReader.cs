using System;
using System.Collections;
using System.IO;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.Networking;

namespace Desdiene.Tools
{
    public class DeviceDataReader : MonoBehaviourExtContainer
    {
        private readonly string _filePath;
        private readonly ICoroutine _dataReading;

        public DeviceDataReader(MonoBehaviourExt mono, string filePath) : base(mono)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} can't be null or empty");
            }

            _filePath = filePath;
            _dataReading = new CoroutineWrap(mono);
        }

        /// <summary>
        /// Загрузить данные с устройства.
        /// Может быть null, если данные не были найдены.
        /// </summary>
        public void Read(Action<bool, string> result)
        {
            _dataReading.StartContinuously(ReadEnumerator(result));
        }

        /// <summary>
        /// Загрузить данные с устройства.
        /// Может быть null, если данные не были найдены.
        /// </summary>
        [Obsolete]
        public void Read(Action<string> result)
        {
            _dataReading.StartContinuously(ReadEnumerator((_, data) => result?.Invoke(data)));
        }

        private IEnumerator ReadEnumerator(Action<bool, string> result)
        {
            var platform = Application.platform;

            switch (platform)
            {
                case RuntimePlatform.Android:
                    yield return _dataReading.StartNested(ReadViaAndroid(result));
                    break;
                case RuntimePlatform.WindowsEditor:
                    result?.Invoke(true, ReadViaEditor());
                    break;
                default:
                    Debug.LogError($"{platform} is unknown platform!");
                    yield return _dataReading.StartNested(ReadViaAndroid(result));
                    break;
            }
        }

        private string ReadViaEditor()
        {
            return File.Exists(_filePath) ? File.ReadAllText(_filePath) : null;
        }

        private IEnumerator ReadViaAndroid(Action<bool, string> result)
        {
            using (UnityWebRequest request = new UnityWebRequest { url = _filePath, downloadHandler = new DownloadHandlerBuffer() })
            {
                yield return request.SendWebRequest();

                if (request.error != null)
                {
                    if (request.responseCode == 404)
                    {
                        Debug.Log($"File {request.url} does not exists.");
                        result?.Invoke(true, null);
                        yield break;
                    }

                    Debug.LogError($"Can't load data from android:\n{request.error}");
                    result?.Invoke(false, null);
                    yield break;
                }

                result?.Invoke(true, request.downloadHandler.text);
            }
        }
    }
}
