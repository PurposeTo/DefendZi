using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.States;
using Desdiene.GameDataAsset.DataLoader.Safe.States.Base;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.Types.AtomicReference;

namespace Desdiene.GameDataAsset.DataLoader.Safe
{
    internal class SafeStorageDataLoader<T> : IStorageDataLoader<T> where T : IData, new()
    {
        private readonly Ref<StorageDataLoaderState<T>> readerWriterState;

        public SafeStorageDataLoader(StorageJsonDataLoader<T> dataStorage)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            //todo: кинет NRE? readerWriterState будет проинициализирован, на момент передачи в параметры?
            readerWriterState = new Ref<StorageDataLoaderState<T>>(new InitialState<T>(readerWriterState, dataStorage));
        }

        public void Load(Action<T> dataCallback)
        {
            readerWriterState.Get().Load(dataCallback);
        }

        public void Save(T data)
        {
            readerWriterState.Get().Save(data);
        }
    }
}
