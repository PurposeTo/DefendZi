using System;
using Desdiene.StateMachines.States;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private abstract class State : IStateEntryExitPoint
        {
            private protected State(StorageAsync<T> it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter() { }
            void IStateEntryExitPoint.OnExit() { }

            protected StorageAsync<T> It { get; }

            public abstract void Load(Action<bool, T> result);
            public abstract void Save(T data, Action<bool> successResult);

            protected void LoadData(Action<bool, T> result)
            {
                try
                {
                    It.LoadData((success, data) =>
                    {
                        if (success) data.TryToRepair();
                        result?.Invoke(success, data);
                    });
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    result?.Invoke(false, default);
                }
            }

            protected void SaveData(T data, Action<bool> successResult)
            {
                if (!data.IsValid())
                {
                    Debug.LogError($"Data is not valid!\n{data}");
                    successResult?.Invoke(false);
                }

                try
                {
                    It.SaveData(data, successResult);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    successResult?.Invoke(false);
                }
            }

            protected void SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
        }
    }
}
