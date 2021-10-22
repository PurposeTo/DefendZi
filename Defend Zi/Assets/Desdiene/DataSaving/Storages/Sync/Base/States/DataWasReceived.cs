﻿namespace Desdiene.DataSaving.Storages
{
    public partial class Storage<T>
    {
        private sealed class DataWasReceived : State
        {
            public DataWasReceived(Storage<T> it) : base(it) { }

            public sealed override bool TryToLoad(out T data) => base.TryToLoad(out data);

            public sealed override bool Save(T data) => base.Save(data);
        }
    }
}
