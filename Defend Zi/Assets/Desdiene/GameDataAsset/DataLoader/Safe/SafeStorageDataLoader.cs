using System;
using System.Collections.Generic;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.GameDataAsset.DataLoader.Safe.States;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.Types.AtomicReference;

namespace Desdiene.GameDataAsset.DataLoader.Safe
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
