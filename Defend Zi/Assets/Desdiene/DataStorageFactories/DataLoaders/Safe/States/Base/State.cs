using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private abstract class State : IStateEntryExitPoint<SafeDataLoader<TData>>
        {
            private readonly IStateSwitcher<State, SafeDataLoader<TData>> _stateSwitcher;
            private readonly SafeDataLoader<TData> _it;

            private protected State(IStateSwitcher<State, SafeDataLoader<TData>> stateSwitcher,
                                    SafeDataLoader<TData> it)
            {
                _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
                _it = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint<SafeDataLoader<TData>>.OnEnter(SafeDataLoader<TData> it) { }
            void IStateEntryExitPoint<SafeDataLoader<TData>>.OnExit(SafeDataLoader<TData> it) { }

            public void Load(Action<TData> dataCallback) => Load(_it, dataCallback);
            public void Save(TData data) => Save(_it, data);

            protected abstract void Load(SafeDataLoader<TData> it, Action<TData> dataCallback);
            protected abstract void Save(SafeDataLoader<TData> it, TData data);

            protected void SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
