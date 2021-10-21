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

        void IStorageAsync<T>.Load(Action<bool, T> result) => CurrentState.Load(result);

        void IStorageAsync<T>.Save(T data, Action<bool> successResult) => CurrentState.Save(data, successResult);

        void IStorageAsync<T>.Clean(Action<bool> successResult) => Clean(successResult);

        private void Clean(Action<bool> successResult)
        {
            try
            {
                CleanData(successResult);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                successResult?.Invoke(false);
            }
        }
        private State CurrentState => _stateSwitcher.CurrentState;

        protected abstract void LoadData(Action<bool, T> result);
        protected abstract void SaveData(T data, Action<bool> successResult);
        protected abstract void CleanData(Action<bool> successResult);
    }
}
