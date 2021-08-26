using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneTypes;
using Desdiene.UnityScenes.LoadingProcess;
using Desdiene.UnityScenes.LoadingProcess.Components;
using SceneTypes;
using UnityEngine;

public class SceneInitorTest : MonoBehaviourExt
{
    private SceneType _sceneType;

    protected override void AwakeExt()
    {
        _sceneType = new Test(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ILoading loading = _sceneType.LoadAsSingle(SceneEnablingAfterLoading.Mode.Allow);
            loading.OnWaitingForAllowingToEnabling += () => loading.SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode.Allow);
        }
    }
}
