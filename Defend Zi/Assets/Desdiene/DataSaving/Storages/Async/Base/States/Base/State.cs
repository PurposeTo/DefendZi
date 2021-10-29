using System;
using Desdiene.StateMachines.States;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private abstract class State : IStateEntryExitPoint
        {
            protected State(StorageAsync<T> it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter() { }
            void IStateEntryExitPoint.OnExit() { }

            protected StorageAsync<T> It { get; }

            public virtual void Read(Action<bool, T> result)
            {
                try
                {
                    It.ReadData((success, data) =>
                    {
                        if (success) data.Repair();
                        result?.Invoke(success, data);
                    });
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    result?.Invoke(false, default);
                }
            }

            public virtual void Update(T data, Action<bool> result)
            {
                if (!data.IsValid())
                {
                    Debug.LogError($"Data is not valid!\n{data}");
                    result?.Invoke(false);
                    return;
                }

                try
                {
                    It.UpdateData(data, result);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    result?.Invoke(false);
                }
            }

            protected void SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
        }
    }
}
