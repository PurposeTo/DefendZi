using System;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private class Initial : State
        {
            public Initial(IStateSwitcher<State> stateSwitcher,
                                   SafeDataLoader<TData> it)
                : base(stateSwitcher, it) { }

            public override void Load(Action<TData> dataCallback)
            {
                It._dataStorage.Load(data =>
                {
                    dataCallback?.Invoke(data);
                    SwitchState<DataWasReceived>();
                });
            }

            public override void Save(TData data, Action<bool> successCallback)
            {
                Debug.Log($"Данные с [{It._dataStorage.StorageName}] еще не были получены. " +
                    $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
            }
        }
    }
}
