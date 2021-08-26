using System;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.LoadingProcess;
using Desdiene.UnityScenes.LoadingProcess.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTypes.Base
{
    /// <summary>
    /// Дочернему классу необходимо дать название, соответствующему названию сцены.
    /// </summary>
    public abstract class SceneType : MonoBehaviourExtContainer
    {
        private readonly string _sceneName;
        private readonly LoadedScenes _loadedScenes;

        public SceneType(MonoBehaviourExt mono) : base(mono)
        {
            ScenesInBuild scenesInBuild = ScenesInBuild.Instance;
            _loadedScenes = LoadedScenes.Instance;

            string sceneName = GetType().Name;

            if (!scenesInBuild.Contains(sceneName))
            {
                throw new TypeLoadException($"Scene with name {sceneName} not found in build! " +
                    $"The class name must match the name of the existing scene and be unique");
            }

            _sceneName = sceneName;

            Debug.Log($"Scene with name \"{_sceneName}\" was found successfully");
        }

        public event Action OnUnloading;
        public event Action OnWaitingForAllowingToEnabling;
        public event Action OnLoadedAndEnabled;


        /// <summary>
        /// Загрузить сцену в одиночном режиме.
        /// Если указать enablingMode = Forbid, то старая сцена не будет выгружена до тех пор, пока новая сцена не будет включена.
        /// </summary>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public LoadingOperation LoadAsSingle(SceneEnablingAfterLoading.Mode alowingEnableMode = SceneEnablingAfterLoading.Mode.Allow)
        {
            return Load(LoadSceneMode.Single, alowingEnableMode);
        }

        public LoadingOperation LoadAsAdditive(SceneEnablingAfterLoading.Mode alowingEnableMode)
        {
            return Load(LoadSceneMode.Additive, alowingEnableMode);
        }

        /// <summary>
        /// Загрузить сцену.
        /// </summary>
        /// <param name="loadSceneMode">Режим загрузки сцены.</param>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public LoadingOperation Load(LoadSceneMode loadSceneMode, SceneEnablingAfterLoading.Mode alowingEnableMode)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            LoadingOperation loading = new LoadingOperation(monoBehaviourExt, loadingOperation, _sceneName);
            loading.SetAllowSceneEnabling(alowingEnableMode);
            loading.OnWaitingForAllowingToEnabling += OnWaitingForAllowingToEnabling;
            loading.OnLoadedAndEnabled += OnLoadedAndEnabled;
            return loading;
        }

        /// <summary>
        /// Выгрузить сцену.
        /// </summary>
        public void Unload()
        {
            if (!_loadedScenes.Contains(_sceneName))
            {
                Debug.LogError($"You can't unload the scene {_sceneName}, because it is not loaded");
            }

            throw new NotImplementedException("Не реализовано. Если будет загружено две одинаковых сцены, то не понятно, какую необходимо выгрузить. Реализовать через выгрузку не по имени, а по Scene scene.");
            AsyncOperation loadingOperation = SceneManager.UnloadSceneAsync(_sceneName);
            loadingOperation.completed += (_) => OnUnloading?.Invoke();
        }
    }
}
