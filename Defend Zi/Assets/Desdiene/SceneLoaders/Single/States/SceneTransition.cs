using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes;
using UnityEngine;

namespace Desdiene.SceneLoaders.Single.States
{
    public class SceneTransition : State
    {
        public SceneTransition(MonoBehaviourExt mono, IStateSwitcher<State> stateSwitcher) : base(mono, stateSwitcher)
        { }

        public override void Load(SceneAsset scene, Action<ILinearProcessesMutator> beforeUnloading, Action afterEnabling)
        {
            Debug.LogError($"Scene transition is in progress now. You can't load scene.");
        }
    }
}
