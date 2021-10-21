using System;
using System.Collections.Generic;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages.Safe
{
    internal partial class SafeStorageData<TData> : IDataStorageOld<TData> where TData : IData, new()
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly IDataStorageOld<TData> _dataStorage;

        private int _lastDataFromStorageHash;

        public SafeStorageData(IDataStorageOld<TData> dataStorage)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

            State initState = new Initial(this);
            List<State> allStates = new List<State>()
            {
                initState,
                new DataWasReceived(this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);
        }

        string IDataStorageOld<TData>.StorageName => _dataStorage.StorageName;

        void IDataStorageOld<TData>.Load(Action<TData> dataCallback)
        {
            CurrentState.Load((data) =>
            {
                _lastDataFromStorageHash = data.GetHashCode();
                dataCallback?.Invoke(data);
            });
        }

        void IDataStorageOld<TData>.Save(TData data, Action<bool> successCallback)
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

        private State CurrentState => _stateSwitcher.CurrentState;
    }
}
