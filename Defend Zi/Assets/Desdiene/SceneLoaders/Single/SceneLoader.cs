using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.SceneLoaders.Single
{
    /// <summary>
    /// Need to be a global singleton!
    /// </summary>
    public class SceneLoader : MonoBehaviourExt
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        protected override void AwakeExt()
        {
            StateSwitcher<State, StateContext> stateSwitcher = new StateSwitcher<State, StateContext>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Default(this, stateSwitcher),
                new SceneTransition(this, stateSwitcher)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Default>();
        }

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        public void Load(SceneAsset scene) => CurrentState.Load(scene);

        public void Reload() => CurrentState.Reload();
    }
}
