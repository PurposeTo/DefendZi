using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneLoaders
{
    public class SingleSceneLoaderOld : MonoBehaviourExtContainer
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly GlobalTimePause isSceneLoading;

        public SingleSceneLoaderOld(
            MonoBehaviourExt mono,
            GlobalTimeScaler timeScaler,
            ILoadingScreen loadingScreen)
            : base(mono)
        {
            isSceneLoading = new GlobalTimePause(mono, timeScaler, "Загрузка сцены");

            this.loadingScreen = loadingScreen;

            SubscribeEvents();
            monoBehaviourExt.OnDestroyed -= UnsubscribeEvents;
        }

        private string loadingSceneName;

        private void SubscribeEvents()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            loadingScreen.AtOpeningEnd += AtOpeningEnd;
            loadingScreen.AtClosingEnd += AtClosingEnd;
        }

        private void UnsubscribeEvents()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            loadingScreen.AtOpeningEnd -= AtOpeningEnd;
            loadingScreen.AtClosingEnd -= AtClosingEnd;
        }

        public void LoadScene(string sceneName)
        {
            loadingSceneName = sceneName;
            isSceneLoading.Start();
            loadingScreen.Close();
        }

        public void ReloadScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            LoadScene(sceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"OnSceneLoaded: {scene.name} in mode: {mode}");
            loadingScreen.Open();
        }

        private void AtOpeningEnd()
        {
            isSceneLoading.Stop();
        }

        private void AtClosingEnd()
        {
            SceneManager.LoadScene(loadingSceneName);
            loadingSceneName = null; // Необходимо очистить поле после загрузки сцены
        }
    }
}
