using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using UnityEngine;
using Zenject;

public class SceneInitorTest : MonoBehaviourExt
{
    [Inject]
    private void Constructor(ScenesInBuild scenesInBuild)
    {
        var game = new SceneTypes.Game(scenesInBuild);
    }
}
