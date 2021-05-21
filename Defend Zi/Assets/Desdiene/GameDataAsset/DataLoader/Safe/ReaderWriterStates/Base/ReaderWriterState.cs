using System;
using Desdiene.AtomicReference;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Storage;

namespace Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates.Base
{
    public abstract class ReaderWriterState<T> where T : GameData, new()
    {
        private protected readonly JsonDataLoader<T> dataStorage;


        private protected ReaderWriterState(JsonDataLoader<T> dataStorage)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            this.dataStorage = dataStorage;
        }

        public abstract void Read(AtomicRef<ReaderWriterState<T>> state, Action<T> dataCallback);
        public abstract void Write(AtomicRef<ReaderWriterState<T>> state, T data);
    }
}
