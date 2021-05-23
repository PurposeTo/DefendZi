using System;
using Desdiene.Types.AtomicReference;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates.Base;
using Desdiene.GameDataAsset.DataLoader.Storage;
using UnityEngine;

namespace Desdiene.GameDataAsset.DataLoader.Safe.ReaderWriterStates
{
    internal class InitialState<T> : ReaderWriterState<T> where T : GameData, new()
    {
        public InitialState(JsonDataLoader<T> dataStorage) : base(dataStorage) { }

        public override void Read(Ref<ReaderWriterState<T>> state, Action<T> dataCallback)
        {
            dataStorage.Load(data =>
            {
                dataCallback?.Invoke(data);
                state.Set(new DataWasReceivedState<T>(dataStorage));
            });


        }

        public override void Write(Ref<ReaderWriterState<T>> state, T data)
        {
            Debug.Log($"Данные с [{dataStorage.Name}] еще не были получены. " +
                $"Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
        }
    }
}
