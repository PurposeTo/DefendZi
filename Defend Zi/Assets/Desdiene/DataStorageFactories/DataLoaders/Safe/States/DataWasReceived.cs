using System;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe
{
    internal partial class SafeDataLoader<TData>
    {
        private class DataWasReceived : State
        {
            public DataWasReceived(SafeDataLoader<TData> it) : base(it) { }

            public override void Load(Action<TData> dataCallback)
            {
                Debug.Log($"Данные с [{It._dataStorage.StorageName}] уже были получены!");
            }

            public override void Save(TData data, Action<bool> successCallback) => It._dataStorage.Save(data, successCallback);
        }
    }
}
