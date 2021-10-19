﻿using System;
using Desdiene.StateMachines.States;

namespace Desdiene.DataStorageFactories.Storages.Safe
{
    internal partial class SafeStorageData<TData>
    {
        private abstract class State : IStateEntryExitPoint
        {
            private protected State(SafeStorageData<TData> it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter() { }
            void IStateEntryExitPoint.OnExit() { }

            protected SafeStorageData<TData> It { get; }

            public abstract void Load(Action<TData> dataCallback);
            public abstract void Save(TData data, Action<bool> successCallback);

            protected void SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
        }
    }
}
