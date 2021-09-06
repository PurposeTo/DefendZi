using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.Types.Processes;
using Desdiene.Coroutines;
using System.Collections;

namespace Desdiene.UnityScenes.Loadings
{
    /// <summary>
    /// Данный класс описывает операцию асинхронной загрузки и включения сцены.
    /// </summary>
    public class LoadingAndEnabling : MonoBehaviourExtContainer, ILoadingAndEnabling
    {
        private readonly AsyncOperation _loadingByUnity;
        private readonly string _sceneName;
        private readonly IProcesses _beforeEnabling;
        private readonly ICoroutine _progressChecking;
        private string _logMessage = "";

        public LoadingAndEnabling(MonoBehaviourExt mono,
                                  AsyncOperation loadingByUnity,
                                  string sceneName,
                                  Action<IProcessesSetter> beforeEnablingAction) : base(mono)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _sceneName = sceneName;
            _beforeEnabling = new ProcessesContainer($"Перед загрузкой сцены \"{_sceneName}\"");
            _loadingByUnity = loadingByUnity ?? throw new ArgumentNullException(nameof(loadingByUnity));
            ProgressInfo = new ProgressInfo(_loadingByUnity);

            SubscribeEvents();
            ForbidSceneEnabling();
            beforeEnablingAction?.Invoke(_beforeEnabling);

            _progressChecking = new CoroutineWrap(mono);
            _progressChecking.StartContinuously(ProgressChecking());
        }

        private Action onLoadedAndEnabled;

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        public event Action OnLoadedAndEnabled
        {
            add { lock (this) { onLoadedAndEnabled += value; } }
            remove { lock (this) { onLoadedAndEnabled -= value; } }
        }

        private ProgressInfo ProgressInfo { get; }
        private float LoadingProgress => ProgressInfo.Progress / 0.9f;

        /* Используется слово "enable" для разделения понятий.
         * Согласно документации unity, может быть "active scene" - главная включенная сцена, при использовании LoadSceneMode.Additive
         * И может быть allowSceneActivation - разрешение включение сцены после загрузки.
         */
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// </summary>
        /// 
        private void AllowSceneEnabling() => _loadingByUnity.allowSceneActivation = true;

        private void ForbidSceneEnabling() => _loadingByUnity.allowSceneActivation = false;

        private IEnumerator ProgressChecking()
        {
            while (!ProgressInfo.IsDone)
            {
                if (ProgressInfo.Equals90Percents && !_beforeEnabling.KeepWaiting)
                {
                    AllowSceneEnabling();
                    break;
                }

                _logMessage = PrintLoadingLog(_logMessage);
                yield return null;
            }
        }

        private void InvokeOnLoadedAndEnabled(AsyncOperation unloadingByUnity) => onLoadedAndEnabled?.Invoke();

        private void SubscribeEvents()
        {
            _loadingByUnity.completed += InvokeOnLoadedAndEnabled;
            monoBehaviourExt.OnDestroyed += UnsubscribeEvents;
        }

        private void UnsubscribeEvents()
        {
            _loadingByUnity.completed -= InvokeOnLoadedAndEnabled;
            monoBehaviourExt.OnDestroyed -= UnsubscribeEvents;
        }

        private string PrintLoadingLog(string logMessage)
        {
            string newLogMessage = $"Loading scene \"{_sceneName}\". Loading progress: {LoadingProgress * 100}%. Loading progress by unity: {_loadingByUnity.progress * 100}%.";
            if (logMessage != newLogMessage)
            {
                logMessage = newLogMessage;
                Debug.Log(logMessage);
            }
            return logMessage;
        }
    }
}
