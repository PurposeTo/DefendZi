using System;
using Desdiene.StateMachines.States;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class Storage<T>
    {
        private abstract class State : IState
        {
           private readonly string _name;

            protected State(Storage<T> it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
                _name = GetType().Name;
            }

            string IState.Name => _name;

            void IState.OnEnter() { }
            void IState.OnExit() { }

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

            public virtual bool TryToUpdate(T data)
            {
                if (!data.IsValid())
                {
                    Debug.LogError($"Data is not valid!\n{data}");
                    return false;
                }

                try
                {
                    return It.TryToUpdateData(data);
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
                    data.Repair();
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
