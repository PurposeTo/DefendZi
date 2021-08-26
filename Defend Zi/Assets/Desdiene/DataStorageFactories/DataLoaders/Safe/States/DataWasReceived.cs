using System;
using Desdiene.DataStorageFactories.Data;
using Desdiene.DataStorageFactories.DataLoaders.FromStorage;
using Desdiene.DataStorageFactories.DataLoaders.Safe.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe.States
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
