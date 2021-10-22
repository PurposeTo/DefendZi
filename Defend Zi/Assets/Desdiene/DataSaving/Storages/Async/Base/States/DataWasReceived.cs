using System;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private sealed class DataWasReceived : State
        {
            public DataWasReceived(StorageAsync<T> it) : base(it) { }

            public override void Load(Action<bool, T> result) => base.Load(result);

            public override void Save(T data, Action<bool> successResult) => base.Save(data, successResult);
        }
    }
}
