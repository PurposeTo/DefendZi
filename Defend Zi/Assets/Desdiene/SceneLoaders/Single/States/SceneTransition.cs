using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Types;
using UnityEngine;

namespace Desdiene.SceneLoaders.Single
{
    public partial class SceneLoader
    {
        private class SceneTransition : State
        {
            public SceneTransition(MonoBehaviourExt mono, SceneLoader it) : base(mono, it) { }

            public override void Load(ISceneAsset scene, Action<IProcessesMutator> beforeUnloading, Action afterEnabling)
            {
                Debug.LogError($"Scene transition is in progress now. You can't load scene.");
            }
        }
    }
}
