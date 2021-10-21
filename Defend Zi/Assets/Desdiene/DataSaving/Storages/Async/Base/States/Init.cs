using System;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private sealed class Init : State
        {
            public Init(StorageAsync<T> it) : base(it) { }

            public override void Load(Action<bool, T> result)
            {
                LoadData((success, data) =>
                {
                    if(success) SwitchState<DataWasReceived>();
                    result?.Invoke(success, data);
                });
            }

            public override void Save(T data, Action<bool> successResult)
            {
                Debug.Log($"Данные с [{It._storageName}] еще не были получены. Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
                successResult?.Invoke(false);
            }
        }
    }
}
