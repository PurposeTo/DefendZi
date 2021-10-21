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

            public abstract bool TryToLoad(out T data);
            public abstract bool Save(T data);

            protected bool TryToLoadData(out T data)
            {
                try
                {
                    return TryToLoadAndRepair(out data);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    data = default;
                    return false;
                }
            }

            protected bool SaveData(T data)
            {
                if (!data.IsValid())
                {
                    Debug.LogError($"Data is not valid!\n{data}");
                    return false;
                }

                try
                {
                    return It.SaveData(data);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                    return false;
                }
            }

            protected void SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();

            private bool TryToLoadAndRepair(out T data)
            {
                if (It.TryToLoadData(out data))
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
