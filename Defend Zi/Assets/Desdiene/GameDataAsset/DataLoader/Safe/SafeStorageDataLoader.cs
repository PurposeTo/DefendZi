using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using System.Collections.Generic;
using System.Linq;

namespace Desdiene.GameDataAsset.DataLoader.Safe
{
    internal class SafeStorageDataLoader<TData> : 
        IStorageDataLoader<TData>, 
        IStateSwitcher<StorageDataLoaderState<TData>> 
        where TData : IData, new()
    {
        private readonly List<StorageDataLoaderState<TData>> _allStates;
        private StorageDataLoaderState<TData> _currentState;

        public SafeStorageDataLoader(StorageJsonDataLoader<TData> dataStorage)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            StorageDataLoaderState<TData> initState = new InitialState<TData>(this, dataStorage);

            _allStates = new List<StorageDataLoaderState<TData>>()
            {
                initState,
                new DataWasReceivedState<TData>(this, dataStorage)
            };
            _currentState = initState;
        }

        public void Load(Action<TData> dataCallback)
        {
            _currentState.Load(dataCallback);
        }

        public void Save(TData data)
        {
            _currentState.Save(data);
        }

        void IStateSwitcher<StorageDataLoaderState<TData>>.Switch<ConcreteStateT>()
        {
            var state = _allStates.FirstOrDefault(s => s is ConcreteStateT);
            _currentState = state;
        }
    }
}
