using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private abstract class State : IStateEntryExitPoint
        {
            private readonly IStateSwitcher<State> _stateSwitcher;

            private protected State(IStateSwitcher<State> stateSwitcher,
                                    SafeDataLoader<TData> it)
            {
                _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter() { }
            void IStateEntryExitPoint.OnExit() { }

            protected SafeDataLoader<TData> It { get; }

            public abstract void Load(Action<TData> dataCallback);
            public abstract void Save(TData data, Action<bool> successCallback);

            protected void SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
