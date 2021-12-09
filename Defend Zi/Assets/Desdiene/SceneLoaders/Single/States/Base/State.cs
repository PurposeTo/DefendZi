using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.States;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Types;

namespace Desdiene.SceneLoaders.Single
{
    public partial class SceneLoader
    {
        private abstract class State : MonoBehaviourExtContainer, IState
        {
           private readonly string _name;

            protected State(MonoBehaviourExt mono, SceneLoader it) : base(mono)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
                _name = GetType().Name;
            }

            string IState.Name => _name;

            void IState.OnEnter()
            {
                OnEnter();
            }

            void IState.OnExit()
            {
                OnExit();
            }

            protected SceneLoader It { get; }


            public abstract void Load(ISceneAsset scene, Action<IProcessesMutator> beforeUnloading, Action afterEnabling);

            protected virtual void OnEnter() { }
            protected virtual void OnExit() { }

            protected State SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
        }
    }
}