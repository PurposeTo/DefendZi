using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes.LoadingOperationAsset;
using UnityEngine;

public class SceneInitorTest : MonoBehaviourExt
{
    private SceneTypes.Base.SceneType _sceneType;

    protected override void AwakeExt()
    {
        _sceneType = new SceneTypes.Test(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadingOperation loading = _sceneType.LoadAsSingle(SceneEnablingAfterLoading.Mode.Forbid);
        }
    }
}
