using System;
using System.Collections.Generic;
using Desdiene.DataStorageFactories.Data;
using Desdiene.DataStorageFactories.DataLoaders.FromStorage;
using Desdiene.DataStorageFactories.DataLoaders.Safe.States;
using Desdiene.DataStorageFactories.DataLoaders.Safe.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal class SafeStorageDataLoader<TData> : IStorageDataLoader<TData> where TData : IData, new()
    {
        private readonly IRef<State<TData>> _refCurrentState = new Ref<State<TData>>();

        public SafeStorageDataLoader(StorageJsonDataLoader<TData> dataStorage)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            StateSwitcher<State<TData>> stateSwitcher = new StateSwitcher<State<TData>>(_refCurrentState);
            List<State<TData>> allStates = new List<State<TData>>()
            {
                new Initial<TData>(stateSwitcher, dataStorage),
                new DataWasReceived<TData>(stateSwitcher, dataStorage)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Initial<TData>>();
        }

        private State<TData> CurrentState => _refCurrentState.Get() ?? throw new ArgumentNullException(nameof(CurrentState));

        public void Load(Action<TData> dataCallback)
        {
            CurrentState.Load(dataCallback);
        }

        public void Save(TData data)
        {
            CurrentState.Save(data);
        }
    }
}
