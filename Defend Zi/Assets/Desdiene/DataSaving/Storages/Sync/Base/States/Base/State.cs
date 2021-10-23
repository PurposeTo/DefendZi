using System;
using Desdiene.StateMachines.States;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class Storage<T>
    {
        private abstract class State : IStateEntryExitPoint
        {
            private protected State(Storage<T> it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter() { }
            void IStateEntryExitPoint.OnExit() { }

            protected Storage<T> It { get; }

            public virtual bool TryToRead(out T data)
            {
                try
                {
                    return TryToReadAndRepair(out data);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    data = default;
                    return false;
                }
            }

            public virtual bool Update(T data)
            {
                if (!data.IsValid())
                {
                    Debug.LogError($"Data is not valid!\n{data}");
                    return false;
                }

                try
                {
                    return It.UpdateData(data);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    return false;
                }
            }

            protected void SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();

            private bool TryToReadAndRepair(out T data)
            {
                if (It.TryToReadData(out data))
                {
                    data.TryToRepair();
                    return true;
                }
                else
                {
                    data = default;
                    return false;
                }
            }
        }
    }
}
