using System;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.LoadingProcess;
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
        /// <param name="enablingMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public Loading LoadAsSingle(AllowingSceneEnabling.Mode enablingMode = AllowingSceneEnabling.Mode.Allow)
        {
            return Load(LoadSceneMode.Single, enablingMode);
        }

        public Loading LoadAsAdditive(AllowingSceneEnabling.Mode enablingMode)
        {
            return Load(LoadSceneMode.Additive, enablingMode);
        }

        /// <summary>
        /// Загрузить сцену.
        /// </summary>
        /// <param name="loadSceneMode">Режим загрузки сцены.</param>
        /// <param name="enablingMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public Loading Load(LoadSceneMode loadSceneMode, AllowingSceneEnabling.Mode enablingMode)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            async.allowSceneActivation = AllowingSceneEnabling.Check(enablingMode);
            Loading loading = new Loading(monoBehaviourExt, async);
            loading.OnWaitingForAllowingToEnabling += OnWaitingForAllowingToEnabling;
            loading.OnLoadedAndEnabled += OnLoadedAndEnabled;
            //loading.SetAllowSceneEnabling(allowSceneActivation);
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
            AsyncOperation async = SceneManager.UnloadSceneAsync(_sceneName);
            async.completed += (_) => OnUnloading?.Invoke();
        }
    }
}
