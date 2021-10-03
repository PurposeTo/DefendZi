﻿using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Types;

namespace Desdiene.SceneLoaders.Single.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;

        public State(MonoBehaviourExt mono, IStateSwitcher<State> stateSwitcher) : base(mono)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
        }

        void IStateEntryExitPoint.OnEnter()
        {
            OnEnter();
        }

        void IStateEntryExitPoint.OnExit()
        {
            OnExit();
        }

        public abstract void Load(ISceneAsset scene, Action<ILinearProcessesMutator> beforeUnloading, Action afterEnabling);

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
