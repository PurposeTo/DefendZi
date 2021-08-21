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

        protected readonly StorageJsonDataLoader<T> _dataStorage;

        private protected State(IStateSwitcher<State<T>> stateSwitcher,
                                StorageJsonDataLoader<T> dataStorage)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            _dataStorage = dataStorage;
        }

        public abstract void Load(Action<T> dataCallback);
        public abstract void Save(T data);

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        protected void SwitchState<stateT>() where stateT : State<T> => _stateSwitcher.Switch<stateT>();
    }
}
