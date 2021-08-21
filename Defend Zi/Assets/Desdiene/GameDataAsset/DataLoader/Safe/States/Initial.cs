using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.StateMachine.StateSwitcher;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States
{
    internal class Initial<T> : State<T> where T : IData, new()
    {

        public Initial(IStateSwitcher<State<T>> stateSwitcher,
                            StorageJsonDataLoader<T> dataStorage)
            : base(stateSwitcher, dataStorage) { }

        public override void Load(Action<T> dataCallback)
        {
            _dataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                SwitchState<DataWasReceived<T>>();
            });
        }

        public override void Save(T data)
        {
            Debug.Log($"Данные с [{_dataStorage.StorageName}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
