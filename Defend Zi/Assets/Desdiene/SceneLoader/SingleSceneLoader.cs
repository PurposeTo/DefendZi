﻿using Assets.Desdiene.SceneLoader;
using Assets.Desdiene.TimeControl.Pause;
using Desdiene.TimeControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneLoader
{
    public class SingleSceneLoader
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly PausableGlobalTime isSceneLoading = new PausableGlobalTime("Загрузка сцены");

        public SingleSceneLoader(ILoadingScreen loadingScreen)
        {
            this.loadingScreen = loadingScreen;

            //todo дабы избежать утечек памяти, необходимо либо отписаться, либо быть синглтоном
            SceneManager.sceneLoaded += OnSceneLoaded;
            loadingScreen.AtOpeningEnd += AtOpeningEnd;
            loadingScreen.AtClosingEnd += AtClosingEnd;
        }

        private string loadingSceneName;

        public void LoadScene(string sceneName)
        {
            loadingSceneName = sceneName;
            isSceneLoading.SetPause(true);
            loadingScreen.Close();
        }

        public void ReloadScene()
        {
            loadingSceneName = SceneManager.GetActiveScene().name; //кеширование необходимо для возможного отслеживания
            LoadScene(loadingSceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"OnSceneLoaded: {scene.name} in mode: {mode}");
            loadingScreen.Open();
        }

        private void AtOpeningEnd()
        {
            isSceneLoading.SetPause(false);
        }

        private void AtClosingEnd()
        {
            SceneManager.LoadScene(loadingSceneName);
            loadingSceneName = null; // Необходимо очистить поле после загрузки сцены
        }
    }
}
