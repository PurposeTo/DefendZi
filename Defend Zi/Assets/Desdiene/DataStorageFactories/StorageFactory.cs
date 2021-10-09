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
        public static IStorage<TData> GetStorage(MonoBehaviourExt mono,
                                                 params StorageJsonData<TData>[] loaders)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (loaders is null) throw new ArgumentNullException(nameof(loaders));

            IStorageData<TData>[] safeReaderWriters = loaders
                .Select(storage => new SafeDataLoader<TData>(storage))
                .ToArray();

            IStorageData<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            return new Storage<TData>(loadersContainer);
        }
    }
}
