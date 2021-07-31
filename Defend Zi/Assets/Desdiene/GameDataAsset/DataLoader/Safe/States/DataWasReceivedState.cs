using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class DataWasReceivedState<T> : StorageDataLoaderState<T> where T : IData, new()
    {
        public DataWasReceivedState(IStateSwitcher<StorageDataLoaderState<T>> stateSwitcher,
                            StorageJsonDataLoader<T> dataStorage)
            : base(stateSwitcher, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            Debug.Log($"Данные с [{_dataStorage.StorageName}] уже были получены!");
        }

        public override void Save(T data) => _dataStorage.Save(data);
    }
}
