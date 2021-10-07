using System;
using Desdiene.DataStorageFactories.DataLoaders.FromStorage;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe.States.Base
{
    internal abstract class State<T> : IStateEntryExitPoint where T : IData, new()
    {
        private readonly IStateSwitcher<State<T>> _stateSwitcher;
        private readonly StorageJsonDataLoader<T> _dataStorage;

        private protected State(IStateSwitcher<State<T>> stateSwitcher,
                                StorageJsonDataLoader<T> dataStorage)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        protected StorageJsonDataLoader<T> DataStorage => _dataStorage;

        void IStateEntryExitPoint.OnEnter() { }
        void IStateEntryExitPoint.OnExit() { }

        public abstract void Load(Action<T> dataCallback);
        public abstract void Save(T data);

        protected void SwitchState<stateT>() where stateT : State<T> => _stateSwitcher.Switch<stateT>();
    }
}
