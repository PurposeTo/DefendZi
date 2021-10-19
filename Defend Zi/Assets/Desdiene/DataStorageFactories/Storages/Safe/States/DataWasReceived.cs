using System;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages.Safe
{
    internal partial class SafeStorageData<TData>
    {
        private class DataWasReceived : State
        {
            public DataWasReceived(SafeStorageData<TData> it) : base(it) { }

            public override void Load(Action<TData> dataCallback)
            {
                Debug.Log($"Данные с [{It._dataStorage.StorageName}] уже были получены!");
            }

            public override void Save(TData data, Action<bool> successCallback) => It._dataStorage.Save(data, successCallback);
        }
    }
}
