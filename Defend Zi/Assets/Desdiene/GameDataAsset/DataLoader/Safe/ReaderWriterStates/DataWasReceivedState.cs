using System;
using Desdiene.AtomicReference;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates.Base;
using Desdiene.GameDataAsset.DataLoader.Storage;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates
{
    internal class DataWasReceivedState<T> : ReaderWriterState<T> where T : GameData, new()
    {
        public DataWasReceivedState(JsonDataLoader<T> dataStorage) : base(dataStorage) { }


        public override void Read(AtomicRef<ReaderWriterState<T>> state, Action<T> dataCallback)
        {
            Debug.Log($"Данные с [{dataStorage.Name}] уже были получены!");
        }

        public override void Write(AtomicRef<ReaderWriterState<T>> state, T data)
        {
            dataStorage.Save(data);
        }
    }
}
