using System;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.DataLoaders.FromStorage;
using Desdiene.DataStorageFactories.DataLoaders.Safe.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataStorageFactories.DataLoaders.Safe.States
{
    internal class Initial<T> : State<T> where T : IData, new()
    {

        public Initial(IStateSwitcher<State<T>> stateSwitcher,
                            StorageJsonDataLoader<T> dataStorage)
            : base(stateSwitcher, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            DataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                SwitchState<DataWasReceived<T>>();
            });
        }

        public override void Save(T data)
        {
            Debug.Log($"Данные с [{DataStorage.StorageName}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
