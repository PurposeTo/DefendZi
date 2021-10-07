using System;
using System.Collections.Generic;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData> : IDataLoader<TData> where TData : IData, new()
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private readonly IDataLoader<TData> _dataStorage;

        private TData _lastDataFromStorage;

        public SafeDataLoader(IDataLoader<TData> dataStorage)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

            var stateSwitcher = new StateSwitcherWithContext<State, SafeDataLoader<TData>>(this, _refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Initial(stateSwitcher, this),
                new DataWasReceived(stateSwitcher, this)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Initial>();
        }

        string IDataLoader<TData>.StorageName => _dataStorage.StorageName;

        void IDataLoader<TData>.Load(Action<TData> dataCallback)
        {
            CurrentState.Load((data) =>
            {
                _lastDataFromStorage = data;
                dataCallback?.Invoke(data);
            });
        }

        void IDataLoader<TData>.Save(TData data)
        {
            if (Equals(data, _lastDataFromStorage)) return;

            // todo из метода Save коллбеком получать успешно сохраненные данные
            CurrentState.Save(data);
        }

        private State CurrentState => _refCurrentState.Value ?? throw new ArgumentNullException(nameof(CurrentState));
    }
}
