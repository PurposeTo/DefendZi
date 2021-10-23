using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    public partial class Storage<T>
    {
        private sealed class Init : State
        {
            public Init(Storage<T> it) : base(it) { }

            public sealed override bool TryToRead(out T data)
            {
                if (base.TryToRead(out data))
                {
                    SwitchState<DataWasReceived>();
                    return true;
                }
                else
                {
                    data = default;
                    return false;
                }
            }

            public sealed override bool Update(T data)
            {
                Debug.Log($"Данные с [{It._storageName}] еще не были получены. Запись невозможна! Иначе данное действие перезапишет еще не полученные данные.");
                return false;
            }
        }
    }
}
