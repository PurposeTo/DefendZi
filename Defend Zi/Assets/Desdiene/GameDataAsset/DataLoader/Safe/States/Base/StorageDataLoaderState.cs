using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.Types.AtomicReference;

namespace Desdiene.GameDataAsset.DataLoader.Safe.States.Base
{
    internal abstract class StorageDataLoaderState<T> where T : GameData, new()
    {
        private protected readonly StorageJsonDataLoader<T> dataStorage;
        private protected readonly Ref<StorageDataLoaderState<T>> state;

        private protected StorageDataLoaderState(Ref<StorageDataLoaderState<T>> state, StorageJsonDataLoader<T> dataStorage)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            this.dataStorage = dataStorage;
        }

        public abstract void Load(Action<T> dataCallback);
        public abstract void Save(T data);
    }
}
