using System;
using System.Collections.Generic;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData> : IStorageData<TData> where TData : IData, new()
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private readonly IStorageData<TData> _dataStorage;

        private int _lastDataFromStorageHash;

        public SafeDataLoader(IStorageData<TData> dataStorage)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

            var stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Initial(stateSwitcher, this),
                new DataWasReceived(stateSwitcher, this)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Initial>();
        }

        string IStorageData<TData>.StorageName => _dataStorage.StorageName;

        void IStorageData<TData>.Load(Action<TData> dataCallback)
        {
            CurrentState.Load((data) =>
            {
                _lastDataFromStorageHash = data.GetHashCode();
                dataCallback?.Invoke(data);
            });
        }

        void IStorageData<TData>.Save(TData data, Action<bool> successCallback)
        {
            if (Equals(data.GetHashCode(), _lastDataFromStorageHash))
            {
                Debug.Log($"[{_dataStorage.StorageName}] Сохраняемые данные совпадают с теми, что уже находятся в хранилище");
                return;
            }

            CurrentState.Save(data, (success) =>
            {
                if (success) _lastDataFromStorageHash = data.GetHashCode();
                Debug.Log($"[{_dataStorage.StorageName}] Данные сохранены со статусом success={success}");

                successCallback?.Invoke(success);
            });
        }

        private State CurrentState => _refCurrentState.Value ?? throw new ArgumentNullException(nameof(CurrentState));
    }
}
