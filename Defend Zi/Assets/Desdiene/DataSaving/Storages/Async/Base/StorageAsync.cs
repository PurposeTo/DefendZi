using System;
using System.Collections.Generic;
using Desdiene.DataSaving.Datas;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public abstract partial class StorageAsync<T> : IStorageAsync<T> where T : IValidData
    {
        private readonly string _storageName;
        private readonly IStateSwitcher<State> _stateSwitcher;

        protected StorageAsync(string storageName)
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

        private event Action<bool, T> OnReaded;
        private event Action<bool> OnUpdated;
        private event Action<bool> OnDeleted;

        event Action<bool, T> IStorageAsync<T>.OnReaded
        {
            add => OnReaded += value;
            remove => OnReaded -= value;
        }

        event Action<bool> IStorageAsync<T>.OnUpdated
        {
            add => OnUpdated += value;
            remove => OnUpdated -= value;
        }

        event Action<bool> IStorageAsync<T>.OnDeleted
        {
            add => OnDeleted += value;
            remove => OnDeleted -= value;
        }

        string IStorageAsync<T>.StorageName => _storageName;

        void IStorageAsync<T>.Read() => CurrentState.Read(InvokeOnReaded);

        void IStorageAsync<T>.Update(T data) => CurrentState.Update(data, InvokeOnUpdated);

        void IStorageAsync<T>.Delete() => Delete(InvokeOnDeleted);
        
        private State CurrentState => _stateSwitcher.CurrentState;

        protected abstract void ReadData(Action<bool, T> result);
        protected abstract void UpdateData(T data, Action<bool> result);
        protected abstract void DeleteData(Action<bool> result);

        private void Delete(Action<bool> result)
        {
            try
            {
                DeleteData(result);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                result?.Invoke(false);
            }
        }

        private void InvokeOnReaded(bool success, T data) => OnReaded?.Invoke(success, data); 
        private void InvokeOnUpdated(bool success) => OnUpdated?.Invoke(success); 
        private void InvokeOnDeleted(bool success) => OnDeleted?.Invoke(success); 
    }
}
