using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Pausables;
using Desdiene.TimeControls.Pausers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneLoaders
{
    public class SingleSceneLoader : MonoBehaviourExtContainer
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly GlobalTimePauser isSceneLoading;

        public SingleSceneLoader(
            MonoBehaviourExt superMono,
            GlobalTimePausable globalTimePauser,
            ILoadingScreen loadingScreen)
            : base(superMono)
        {
            isSceneLoading = new GlobalTimePauser(superMono, globalTimePauser, "Загрузка сцены");

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
            isSceneLoading.SetPause(true);
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
            isSceneLoading.SetPause(false);
        }

        private void AtClosingEnd()
        {
            SceneManager.LoadScene(loadingSceneName);
            loadingSceneName = null; // Необходимо очистить поле после загрузки сцены
        }
    }
}
