using System;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private sealed class DataWasReceived : State
        {
            public DataWasReceived(StorageAsync<T> it) : base(it) { }

            public override void Load(Action<bool, T> result) => LoadData(result);

            public override void Save(T data, Action<bool> successResult) => SaveData(data, successResult);
        }
    }
}
