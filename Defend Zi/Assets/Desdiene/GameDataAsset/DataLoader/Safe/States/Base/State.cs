using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States.Base
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
