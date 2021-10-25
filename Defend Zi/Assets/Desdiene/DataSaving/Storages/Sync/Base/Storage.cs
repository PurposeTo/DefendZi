using System;
using System.Collections.Generic;
using Desdiene.DataSaving.Datas;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract partial class Storage<T> : IStorage<T> where T : IValidData
    {
        private readonly string _storageName;
        private readonly IStateSwitcher<State> _stateSwitcher;

        protected Storage(string storageName)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                throw new ArgumentException($"{nameof(storageName)} can't be null or empty");
            }

            _storageName = storageName;

            State initState = new Inited(this);
            List<State> allStates = new List<State>()
            {
                initState,
                new DataWasReceived(this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);
        }

        string IStorage<T>.StorageName => _storageName;

        bool IStorage<T>.TryToRead(out T data) => CurrentState.TryToRead(out data);

        bool IStorage<T>.Update(T data) => CurrentState.Update(data);

        bool IStorage<T>.TryToDelete() => TryToDelete();

        private State CurrentState => _stateSwitcher.CurrentState;

        protected abstract bool TryToReadData(out T data);
        protected abstract bool UpdateData(T data);
        protected abstract bool TryToDeleteData();


        private bool TryToDelete()
        {
            try
            {
                return TryToDeleteData();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return false;
            }
        }
    }
}
