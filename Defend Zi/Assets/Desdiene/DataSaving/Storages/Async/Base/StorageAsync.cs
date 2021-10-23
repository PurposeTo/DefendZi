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

            State initState = new Init(this);
            List<State> allStates = new List<State>()
            {
                initState,
                new DataWasReceived(this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);
        }

        string IStorageAsync<T>.StorageName => _storageName;

        void IStorageAsync<T>.Read(Action<bool, T> result) => CurrentState.Read(result);

        void IStorageAsync<T>.Update(T data, Action<bool> successResult) => CurrentState.Update(data, successResult);

        void IStorageAsync<T>.Delete(Action<bool> successResult) => Delete(successResult);

        private void Delete(Action<bool> successResult)
        {
            try
            {
                DeleteData(successResult);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                successResult?.Invoke(false);
            }
        }
        private State CurrentState => _stateSwitcher.CurrentState;

        protected abstract void ReadData(Action<bool, T> result);
        protected abstract void UpdateData(T data, Action<bool> successResult);
        protected abstract void DeleteData(Action<bool> successResult);
    }
}
