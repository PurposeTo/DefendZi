using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.Types.AtomicReference;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States.Base
{
    internal abstract class StorageDataLoaderState<T> where T : IData, new()
    {
        private protected readonly StorageJsonDataLoader<T> _dataStorage;
        private protected readonly Ref<StorageDataLoaderState<T>> _state;

        private protected StorageDataLoaderState(Ref<StorageDataLoaderState<T>> state, StorageJsonDataLoader<T> dataStorage)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            _dataStorage = dataStorage;
        }

        public abstract void Load(Action<T> dataCallback);
        public abstract void Save(T data);
    }
}
