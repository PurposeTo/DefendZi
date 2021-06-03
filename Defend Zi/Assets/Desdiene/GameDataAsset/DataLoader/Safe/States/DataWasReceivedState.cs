using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.Types.AtomicReference;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class DataWasReceivedState<T> : StorageDataLoaderState<T> where T : GameData, new()
    {
        public DataWasReceivedState(Ref<StorageDataLoaderState<T>> state, StorageJsonDataLoader<T> dataStorage)
            : base(state, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            Debug.Log($"Данные с [{dataStorage.StorageName}] уже были получены!");
        }

        public override void Save(T data)
        {
            dataStorage.Save(data);
        }
    }
}
