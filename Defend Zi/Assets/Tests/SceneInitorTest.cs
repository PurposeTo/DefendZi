using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneTypes;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Unloadings;
using SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitorTest : MonoBehaviourExt
{
    private SceneType _sceneType;

    protected override void AwakeExt()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Debug.Log($"Была загружена новая сцена!");
        };
        _sceneType = new Test(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene firstLoadedScene = LoadedScenes.Instance.Get()[0];

            ILoading loading = _sceneType.LoadAsAdditive(SceneEnablingAfterLoading.Mode.Allow);
            loading.OnLoadedAndEnabled += () =>
            {
                Scene[] scenes = LoadedScenes.Instance.Get();

                IUnloading unloading = new Unloading(SceneManager.UnloadSceneAsync(firstLoadedScene));
                unloading.OnUnloaded += () =>
                {
                    Debug.Log($"Сцена загружена - {scenes[0].isLoaded}. Первое сравнение: {scenes[0] == firstLoadedScene}");
                    Debug.Log($"Сцена загружена - {scenes[1].isLoaded}. Второе сравнение: {scenes[1] == firstLoadedScene}");
                };
            };
        }
    }
}
