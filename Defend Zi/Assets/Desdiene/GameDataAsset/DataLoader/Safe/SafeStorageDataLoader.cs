using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.StateMachine.StateSwitcher;

namespace Desdiene.GameDataAsset.DataLoader.Safe
{
    internal class SafeStorageDataLoader<TData> : 
        IStorageDataLoader<TData>, 
        IStateSwitcher<State<TData>> 
        where TData : IData, new()
    {
        private readonly List<State<TData>> _allStates;

        public SafeStorageDataLoader(StorageJsonDataLoader<TData> dataStorage)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            State<TData> initState = new Initial<TData>(this, dataStorage);
            _allStates = new List<State<TData>>()
            {
                initState,
                new DataWasReceived<TData>(this, dataStorage)
            };
            _currentState = initState;
        }

        private State<TData> _currentState;

        public void Load(Action<TData> dataCallback)
        {
            _currentState.Load(dataCallback);
        }

        public void Save(TData data)
        {
            _currentState.Save(data);
        }

        void IStateSwitcher<State<TData>>.Switch<ConcreteStateT>()
        {
            var state = _allStates.FirstOrDefault(s => s is ConcreteStateT);
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
    }
}
