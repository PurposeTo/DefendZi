using Desdiene.SceneLoader;
using Desdiene.TimeControl.Pause;
using Desdiene.TimeControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Desdiene.Container;

namespace Desdiene.SceneLoader
{
    public class SingleSceneLoader : MonoBehaviourContainer
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly PausableGlobalTime isSceneLoading;

        public SingleSceneLoader(MonoBehaviour monoBehaviour, ILoadingScreen loadingScreen) : base(monoBehaviour)
        {
            isSceneLoading = new PausableGlobalTime(monoBehaviour, "Загрузка сцены");

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
