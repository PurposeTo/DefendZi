using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class DataWasReceived<T> : State<T> where T : IData, new()
    {
        public DataWasReceived(IStateSwitcher<State<T>> stateSwitcher,
                            StorageJsonDataLoader<T> dataStorage)
            : base(stateSwitcher, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            Debug.Log($"Данные с [{DataStorage.StorageName}] уже были получены!");
        }

        public override void Save(T data) => DataStorage.Save(data);
    }
}
