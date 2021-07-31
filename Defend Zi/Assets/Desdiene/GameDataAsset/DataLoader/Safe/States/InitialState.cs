using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class InitialState<T> : StorageDataLoaderState<T> where T : IData, new()
    {

        public InitialState(IStateSwitcher<StorageDataLoaderState<T>> stateSwitcher,
                            StorageJsonDataLoader<T> dataStorage)
            : base(stateSwitcher, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            _dataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                SwitchState<DataWasReceivedState<T>>();
            });
        }

        public override void Save(T data)
        {
            Debug.Log($"Данные с [{_dataStorage.StorageName}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
