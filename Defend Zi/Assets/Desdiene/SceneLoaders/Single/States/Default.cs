using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.SceneLoaders.Single.States
{
    public class Default : State
    {
        public Default(MonoBehaviourExt mono, IStateSwitcher<State, StateContext> stateSwitcher) : base(mono, stateSwitcher) { }

        public override void Load(SceneAsset scene)
        {
            SceneToLoad = scene;
            SwitchState<SceneTransition>();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
