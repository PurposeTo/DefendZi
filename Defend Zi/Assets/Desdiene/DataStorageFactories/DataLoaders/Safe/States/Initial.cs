using System;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private class Initial : State
        {
            public Initial(IStateSwitcher<State, SafeDataLoader<TData>> stateSwitcher,
                                   SafeDataLoader<TData> it)
                : base(stateSwitcher, it) { }

            protected override void Load(SafeDataLoader<TData> it, Action<TData> dataCallback)
            {
                it._dataStorage.Load(data =>
                {
                    dataCallback?.Invoke(data);
                    SwitchState<DataWasReceived>();
                });
            }

            protected override void Save(SafeDataLoader<TData> it, TData data, Action<bool> successCallback)
            {
                Debug.Log($"Данные с [{it._dataStorage.StorageName}] еще не были получены. " +
                    $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
            }
        }
    }
}
