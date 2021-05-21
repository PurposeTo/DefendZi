using System;
using Desdiene.GameDataAsset.Data;
using Desdiene.SuperMonoBehaviourAsset;

namespace Desdiene.GameDataAsset.DataLoader
{
    internal class DataReaderWritersContainer<T> : 
        ReaderWriter<T>,
        IReaderStorage<T>, 
        IWriterStorage<T>
        where T : GameData
    {
        private readonly ReaderWriter<T>[] storages;

        public DataReaderWritersContainer(SuperMonoBehaviour superMonoBehaviour, params ReaderWriter<T>[] storages)
            : base(superMonoBehaviour)
        {
            this.storages = storages;
        }

        public override void Load(Action<T> data)
        {
            Array.ForEach(storages, storage => storage.Load(data));
        }

        public override void Save(T data)
        {
            Array.ForEach(storages, storage => storage.Save(data));
        }
    }
}
