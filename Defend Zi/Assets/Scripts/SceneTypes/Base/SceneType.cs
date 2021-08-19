using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.PercentAsset;
using Desdiene.UnityScenes;
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
        public event Action OnLoaded;

        // public bool IsLoading => throw new NotImplementedException();
        public bool IsLoaded => throw new NotImplementedException();

        private Percent _loadingProgress = new Percent();

        public void Load()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            new CoroutineWrap(monoBehaviourExt).StartContinuously(WaitSceneLoading(async));
        }

        private IEnumerator WaitSceneLoading(AsyncOperation async)
        {
            async.allowSceneActivation = false;
            Debug.Log($"КРЯ");

            yield return new WaitUntil(() => IsLoading(async));
            Debug.Log($"Загружено сцен: {_loadedScenes.Get().Length}");
        }

        /*
         * Существует три состояния загрузки сцены:
         * 1. Сцена в процессе загрузки
         * 2. Сцена загружена и ожидает включения
         *     Исходя из документации, async.progress равен .9f И async.allowSceneActivation = false) 
         * 3. Сцена загружена и включена
         *     async.progress равен 1 И async.allowSceneActivation = true
         */

        //todo переписать с учетом состояния загрузки сцен
        private bool IsLoading(AsyncOperation async)
        {
            _loadingProgress.Set(Mathf.Clamp01(async.progress / .9f));
            Debug.Log($"Загрузка сцены завершена на {_loadingProgress * 100}%");

            return _loadingProgress.IsMax;
        }
    }
}

