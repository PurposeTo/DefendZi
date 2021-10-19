using System;
using System.Linq;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.DataStorageFactories.Storages.Json;
using Desdiene.DataStorageFactories.Storages.Safe;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.DataContainers;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.DataStorageFactories
{
    public static class StorageFactory<TData> where TData : IData, IDataCombiner<TData>, new()
    {
        public static IDataContainer<TData> GetStorage(MonoBehaviourExt mono,
                                                 params StorageJsonData<TData>[] storages)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (storages is null) throw new ArgumentNullException(nameof(storages));

            IDataStorageOld<TData>[] safeReaderWriters = storages
                .Select(storage => new SafeStorageData<TData>(storage))
                .ToArray();

            IDataStorageOld<TData> loadersContainer = new StoragesContainer<TData>(safeReaderWriters);
            return new DataContainer<TData>(loadersContainer);
        }
    }
}
