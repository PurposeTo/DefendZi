using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneTypes;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.SceneTypes;
using Desdiene.UnityScenes.Unloadings;
using SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitorTest : MonoBehaviourExt
{
    private SceneAsset _sceneType;

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
            SceneObject firstLoadedScene = LoadedScenes.Instance.Get()[0];

            ILoadingAndEnabling loading = _sceneType.LoadAsAdditive(SceneEnablingAfterLoading.Mode.Allow);
            loading.OnLoadedAndEnabled += (_) =>
            {
                SceneObject[] scenes = LoadedScenes.Instance.Get();

                IUnloading unloading = new Unloading(SceneManager.UnloadSceneAsync(firstLoadedScene.UnityScene));
                unloading.OnUnloaded += () =>
                {
                    Debug.Log($"Сцена загружена - {scenes[0].IsLoadedAndEnabled}. Первое сравнение: {scenes[0] == firstLoadedScene}");
                    Debug.Log($"Сцена загружена - {scenes[1].IsLoadedAndEnabled}. Второе сравнение: {scenes[1] == firstLoadedScene}");
                };
            };
        }
    }
}
