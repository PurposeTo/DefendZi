using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.Types.AtomicReference;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class InitialState<T> : StorageDataLoaderState<T> where T : IData, new()
    {
        public InitialState(Ref<StorageDataLoaderState<T>> state, StorageJsonDataLoader<T> dataStorage) : base(state, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            _dataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                _state.Set(new DataWasReceivedState<T>(_state, _dataStorage));
            });
        }

        public override void Save(T data)
        {
            Debug.Log($"Данные с [{_dataStorage.StorageName}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
