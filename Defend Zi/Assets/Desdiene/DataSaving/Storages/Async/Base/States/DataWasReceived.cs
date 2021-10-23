using System;

namespace Desdiene.DataSaving.Storages
{
    public partial class StorageAsync<T>
    {
        private sealed class DataWasReceived : State
        {
            public DataWasReceived(StorageAsync<T> it) : base(it) { }

            public override void Read(Action<bool, T> result) => base.Read(result);

            public override void Update(T data, Action<bool> successResult) => base.Update(data, successResult);
        }
    }
}
