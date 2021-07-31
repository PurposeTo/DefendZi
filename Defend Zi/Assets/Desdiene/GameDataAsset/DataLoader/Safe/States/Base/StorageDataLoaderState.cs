using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.FromStorage;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States.Base
{
    internal abstract class StorageDataLoaderState<T> where T : IData, new()
    {
        private readonly IStateSwitcher<StorageDataLoaderState<T>> _stateSwitcher;

        protected readonly StorageJsonDataLoader<T> _dataStorage;

        private protected StorageDataLoaderState(IStateSwitcher<StorageDataLoaderState<T>> stateSwitcher,
                                                 StorageJsonDataLoader<T> dataStorage)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            _dataStorage = dataStorage;
        }

        public abstract void Load(Action<T> dataCallback);
        public abstract void Save(T data);

        protected void SwitchState<stateT>() where stateT : StorageDataLoaderState<T> => _stateSwitcher.Switch<stateT>();
    }
}
