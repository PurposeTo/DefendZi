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

            ILoadingAndEnabling loading = _sceneType.LoadAsSingle(SceneEnablingAfterLoading.Mode.Forbid);
            loading.OnLoaded += (_) => loading.AllowSceneEnabling();

        }
    }
}
