using System;
using Desdiene.Types.AtomicReference;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates;
using Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates.Base;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.SuperMonoBehaviourAsset;

namespace Desdiene.GameDataAsset.DataLoader.Safe
{
    //todo Не нужен superMonoBehaviour! Заменить наследование от класса на реализацию интерфейса
    internal class SafeReaderWriter<T> : ReaderWriter<T> where T : GameData, new()
    {
        private readonly Ref<ReaderWriterState<T>> readerWriterState;

        public SafeReaderWriter(SuperMonoBehaviour superMonoBehaviour, JsonDataLoader<T> dataStorage) : base(superMonoBehaviour)
        {
            if (dataStorage is null) throw new ArgumentNullException(nameof(dataStorage));

            readerWriterState = new Ref<ReaderWriterState<T>>(new InitialState<T>(dataStorage));
        }

        public override void Load(Action<T> dataCallback)
        {
            readerWriterState.Get().Read(readerWriterState, dataCallback);
        }

        public override void Save(T data)
        {
            readerWriterState.Get().Write(readerWriterState, data);
        }

    }
}
