using System;
using System.Linq;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.DataStorageFactories.DataLoaders.Json;
using Desdiene.DataStorageFactories.DataLoaders.Safe;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.Storages;
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

            IStorageData<TData>[] safeReaderWriters = storages
                .Select(storage => new SafeDataLoader<TData>(storage))
                .ToArray();

            IStorageData<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            return new DataContainer<TData>(loadersContainer);
        }
    }
}
