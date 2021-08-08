using System.Linq;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Safe;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.GameDataAsset.Storage;
using Desdiene.MonoBehaviourExtension;
using Desdiene.GameDataAsset.Combiner;

namespace Desdiene.GameDataAsset
{
    public static class DataAssetIniter<TData> where TData : IData, new()
    {
        public static IStorage<TData> GetStorage(MonoBehaviourExt mono,
                                                 IDataCombiner<TData> combiner,
                                                 params StorageJsonDataLoader<TData>[] loaders)
        {
            if (mono == null) throw new System.ArgumentNullException(nameof(mono));
            if (combiner is null) throw new System.ArgumentNullException(nameof(combiner));
            if (loaders is null) throw new System.ArgumentNullException(nameof(loaders));

            IStorageDataLoader<TData>[] safeReaderWriters = loaders
                .Select(storage => new SafeStorageDataLoader<TData>(storage))
                .ToArray();

            IStorageDataLoader<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            return new Storage<TData>(mono, combiner, loadersContainer);
        }
    }
}
