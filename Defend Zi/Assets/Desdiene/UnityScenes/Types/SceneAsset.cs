using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Loadings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes.Types
{
    /// <summary>
    /// Описывает файл "юнити сцена". НЕ описывает загруженную сцену.
    /// Дочернему классу необходимо дать название, соответствующему названию сцены.
    /// </summary>
    internal class SceneAsset : MonoBehaviourExtContainer, ISceneAsset
    {
        private readonly string _sceneName;

        internal SceneAsset(MonoBehaviourExt mono, string sceneName) : base(mono)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _sceneName = sceneName;
        }

        /// <summary>
        /// Загрузить сцену в одиночном режиме.
        /// Если указать enablingMode = Forbid, то старая сцена не будет выгружена до тех пор, пока новая сцена не будет включена.
        /// </summary>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        ILoadingAndEnabling ISceneAsset.LoadAsSingle(Action<IProcessesMutator> beforeEnabling)
        {
            return Load(LoadSceneMode.Single, beforeEnabling);
        }

        ILoadingAndEnabling ISceneAsset.LoadAsAdditive(Action<IProcessesMutator> beforeEnabling)
        {
            return Load(LoadSceneMode.Additive, beforeEnabling);
        }

        /// <summary>
        /// Загрузить сцену.
        /// </summary>
        /// <param name="loadSceneMode">Режим загрузки сцены.</param>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        ILoadingAndEnabling ISceneAsset.Load(LoadSceneMode loadSceneMode, Action<IProcessesMutator> beforeEnabling)
        {
            return Load(loadSceneMode, beforeEnabling);
        }

        private ILoadingAndEnabling Load(LoadSceneMode loadSceneMode, Action<IProcessesMutator> beforeEnabling)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            LoadingAndEnabling loading = new LoadingAndEnabling(MonoBehaviourExt, loadingOperation, _sceneName, beforeEnabling);
            return loading;
        }
    }
}
