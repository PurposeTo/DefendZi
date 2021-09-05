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
        private readonly IProcess _loading;
        private readonly IProcess _enabling;
        private readonly ICoroutine _progressChecking;
        private string _logMessage = "";

        public LoadingAndEnabling(MonoBehaviourExt mono,
                                  AsyncOperation loadingByUnity,
                                  string sceneName,
                                  SceneEnablingAfterLoading.Mode alowingEnableMode) : base(mono)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _sceneName = sceneName;
            _loadingByUnity = loadingByUnity ?? throw new ArgumentNullException(nameof(loadingByUnity));
            ProgressInfo = new ProgressInfo(_loadingByUnity);
            bool isAllow = SceneEnablingAfterLoading.IsAllow(alowingEnableMode);
            _loadingByUnity.allowSceneActivation = isAllow;

            // проверка текущего состояния
            _loading = new Process($"Загрузка сцены \"{_sceneName}\"");
            SetActualLoadingState();

            _enabling = new Process($"Включение сцены \"{_sceneName}\"");
            SetActualEnablingState();
            _loadingByUnity.completed += (_) => _enabling.Complete();

            _progressChecking = new CoroutineWrap(mono);
            _progressChecking.StartContinuously(ProgressChecking());

        }

        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        public event Action<ILoadingAndEnablingGetterNotifier> OnLoaded
        {
            add => _loading.OnCompleted += () => value?.Invoke(this);
            remove => _loading.OnCompleted += () => value?.Invoke(this);
        }

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        public event Action<ILoadingAndEnablingGetterNotifier> OnLoadedAndEnabled
        {
            add => _enabling.OnCompleted += () => value?.Invoke(this);
            remove => _enabling.OnCompleted += () => value?.Invoke(this);
        }

        public IProcessGetterNotifier Loading => _loading;
        public IProcessGetterNotifier Enabling => _enabling;

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
        public void AllowSceneEnabling()
        {
            _loadingByUnity.allowSceneActivation = true;
        }

        private IEnumerator ProgressChecking()
        {
            while (!ProgressInfo.IsDone)
            {
                SetActualLoadingState();
                SetActualEnablingState();

                _logMessage = PrintLoadingLog(_logMessage);
                yield return null;
            }
        }

        private void SetActualLoadingState()
        {
            IProcess _ = ProgressInfo.LessThan90Percents
                ? _loading.Start()
                : _loading.Complete();
        }

        private void SetActualEnablingState()
        {
            IProcess _ = ProgressInfo.Between90And100PercentsExcluding
                ? _enabling.Start()
                : ProgressInfo.Equals100Percents
                ? _enabling.Complete()
                : _enabling;
        }

        private string PrintLoadingLog(string logMessage)
        {
            string newLogMessage = $"Loading scene \"{_sceneName}\". Loading progress: {LoadingProgress * 100}%. Loading progress by unity: {_loadingByUnity.progress * 100}%. IsLoading: {_loading.KeepWaiting}. IsEnabling: {_enabling.KeepWaiting}";
            if (logMessage != newLogMessage)
            {
                logMessage = newLogMessage;
                Debug.Log(logMessage);
            }
            return logMessage;
        }
    }
}
