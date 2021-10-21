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

            State initState = new Init(this);
            List<State> allStates = new List<State>()
            {
                initState,
                new DataWasReceived(this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);
        }

        string IStorage<T>.StorageName => _storageName;

        bool IStorage<T>.TryToLoad(out T data) => CurrentState.TryToLoad(out data);

        bool IStorage<T>.Save(T data) => CurrentState.Save(data);

        bool IStorage<T>.TryToClean() => TryToClean();

        private State CurrentState => _stateSwitcher.CurrentState;

        protected abstract bool TryToLoadData(out T data);
        protected abstract bool SaveData(T data);
        protected abstract bool TryToCleanData();


        private bool TryToClean()
        {
            try
            {
                return TryToCleanData();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return false;
            }
        }
    }
}
