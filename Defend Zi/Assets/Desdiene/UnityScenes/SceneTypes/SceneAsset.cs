using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneTypes
{
    /// <summary>
    /// Описывает файл "юнити сцена". НЕ описывает загруженную сцену.
    /// Дочернему классу необходимо дать название, соответствующему названию сцены.
    /// </summary>
    public class SceneAsset : MonoBehaviourExtContainer
    {
        private readonly string _sceneName;
        private readonly LoadedScenes _loadedScenes;

        public SceneAsset(MonoBehaviourExt mono) : base(mono)
        {
            ScenesInBuild scenesInBuild = ScenesInBuild.Instance;
            _sceneName = AssertSceneName(scenesInBuild, GetType().Name);
        }

        public SceneAsset(MonoBehaviourExt mono, string sceneName) : base(mono)
        {
            ScenesInBuild scenesInBuild = ScenesInBuild.Instance;
            _sceneName = AssertSceneName(scenesInBuild, sceneName);
        }

        /// <summary>
        /// Загрузить сцену в одиночном режиме.
        /// Если указать enablingMode = Forbid, то старая сцена не будет выгружена до тех пор, пока новая сцена не будет включена.
        /// </summary>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public ILoadingAndEnabling LoadAsSingle(Action<IProcessesSetter> beforeEnabling)
        {
            return Load(LoadSceneMode.Single, beforeEnabling);
        }

        public ILoadingAndEnabling LoadAsAdditive(Action<IProcessesSetter> beforeEnabling)
        {
            return Load(LoadSceneMode.Additive, beforeEnabling);
        }

        /// <summary>
        /// Загрузить сцену.
        /// </summary>
        /// <param name="loadSceneMode">Режим загрузки сцены.</param>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public ILoadingAndEnabling Load(LoadSceneMode loadSceneMode, Action<IProcessesSetter> beforeEnabling)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            LoadingAndEnabling loading = new LoadingAndEnabling(monoBehaviourExt, loadingOperation, _sceneName, beforeEnabling);
            return loading;
        }

        private string AssertSceneName(ScenesInBuild scenesInBuild, string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            if (!scenesInBuild.Contains(sceneName))
            {
                throw new TypeLoadException($"Scene with name {sceneName} not found in build! " +
                    $"The class name must match the name of the existing scene and be unique");
            }

            Debug.Log($"Scene with name \"{sceneName}\" was found successfully");
            return sceneName;
        }
    }
}
