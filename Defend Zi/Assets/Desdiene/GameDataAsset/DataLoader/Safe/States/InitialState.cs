using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.Types.AtomicReference;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class InitialState<T> : StorageDataLoaderState<T> where T : GameData, new()
    {
        public InitialState(Ref<StorageDataLoaderState<T>> state, StorageJsonDataLoader<T> dataStorage) : base(state, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            dataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                state.Set(new DataWasReceivedState<T>(state, dataStorage));
            });
        }

        public override void Save(T data)
        {
            Debug.Log($"Данные с [{dataStorage.StorageName}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
