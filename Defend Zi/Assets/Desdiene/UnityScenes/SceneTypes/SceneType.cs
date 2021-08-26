﻿using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Unloadings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneTypes
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
        public event Action OnUnloaded;
        public event Action OnWaitingForAllowingToEnabling;
        public event Action OnLoadedAndEnabled;


        /// <summary>
        /// Загрузить сцену в одиночном режиме.
        /// Если указать enablingMode = Forbid, то старая сцена не будет выгружена до тех пор, пока новая сцена не будет включена.
        /// </summary>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public ILoading LoadAsSingle(SceneEnablingAfterLoading.Mode alowingEnableMode = SceneEnablingAfterLoading.Mode.Allow)
        {
            return Load(LoadSceneMode.Single, alowingEnableMode);
        }

        public ILoading LoadAsAdditive(SceneEnablingAfterLoading.Mode alowingEnableMode)
        {
            return Load(LoadSceneMode.Additive, alowingEnableMode);
        }

        /// <summary>
        /// Загрузить сцену.
        /// </summary>
        /// <param name="loadSceneMode">Режим загрузки сцены.</param>
        /// <param name="alowingEnableMode">Режим разрешения на включение сцены после загрузки.</param>
        /// <returns>Объект, описывающий процесс ожидания.</returns>
        public ILoading Load(LoadSceneMode loadSceneMode, SceneEnablingAfterLoading.Mode alowingEnableMode)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            Loading loading = new Loading(monoBehaviourExt, loadingOperation, _sceneName);
            loading.SetAllowSceneEnabling(alowingEnableMode);
            loading.OnWaitingForAllowingToEnabling += OnWaitingForAllowingToEnabling;
            loading.OnLoadedAndEnabled += OnLoadedAndEnabled;
            return loading;
        }

        /// <summary>
        /// Выгрузить сцену.
        /// </summary>
        public IUnloading Unload()
        {
            if (!_loadedScenes.Contains(_sceneName))
            {
                Debug.LogError($"You can't unload the scene {_sceneName}, because it is not loaded");
            }

            throw new NotImplementedException("Не реализовано. Если будет загружено две одинаковых сцены, то не понятно, какую необходимо выгрузить. Реализовать через выгрузку не по имени, а по Scene scene.");
            OnUnloading?.Invoke();
            AsyncOperation unloadingOperation = SceneManager.UnloadSceneAsync(_sceneName);
            Unloading unloading = new Unloading(unloadingOperation);

            //todo: данный код не учитывает, что выгрузка сцены может произойти произвольно - например, при загрузке сцены с помощью Single мода.
            unloading.OnUnloaded += OnUnloaded;
        }
    }
}
