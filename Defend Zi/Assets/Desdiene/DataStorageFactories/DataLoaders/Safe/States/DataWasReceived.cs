using System;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private class DataWasReceived : State
        {
            public DataWasReceived(IStateSwitcher<State, SafeDataLoader<TData>> stateSwitcher,
                                   SafeDataLoader<TData> it)
                : base(stateSwitcher, it) { }

            protected override void Load(SafeDataLoader<TData> it, Action<TData> dataCallback)
            {
                Debug.Log($"Данные с [{it._dataStorage.StorageName}] уже были получены!");
            }

            protected override void Save(SafeDataLoader<TData> it,
                                         TData data,
                                         Action<bool> successCallback) => it._dataStorage.Save(data, successCallback);
        }
    }
}
