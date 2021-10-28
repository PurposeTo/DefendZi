namespace Desdiene.DataSaving.Storages
{
    public partial class Storage<T>
    {
        private sealed class DataWasReceived : State
        {
            public DataWasReceived(Storage<T> it) : base(it) { }

            public sealed override bool TryToRead(out T data) => base.TryToRead(out data);

            public sealed override bool TryToUpdate(T data) => base.TryToUpdate(data);
        }
    }
}
