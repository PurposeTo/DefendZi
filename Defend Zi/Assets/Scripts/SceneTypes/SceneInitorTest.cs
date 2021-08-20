using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.LoadingProcess;
using UnityEngine;
using Zenject;

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
            Loading loading = _sceneType.LoadAsSingle();
        }
    }
}
