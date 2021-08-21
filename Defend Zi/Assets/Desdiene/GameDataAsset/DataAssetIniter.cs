using System.Linq;
using Desdiene.GameDataAsset.Combiner;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.FromStorage;
using Desdiene.GameDataAsset.DataLoader.Safe;
using Desdiene.GameDataAsset.Storage;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.GameDataAsset
{
    public static class DataAssetIniter<TData> where TData : IData, IDataCombiner<TData>, new()
    {
        public static IStorage<TData> GetStorage(MonoBehaviourExt mono,
                                                 params StorageJsonDataLoader<TData>[] loaders)
        {
            if (mono == null) throw new System.ArgumentNullException(nameof(mono));
            if (loaders is null) throw new System.ArgumentNullException(nameof(loaders));

            IStorageDataLoader<TData>[] safeReaderWriters = loaders
                .Select(storage => new SafeStorageDataLoader<TData>(storage))
                .ToArray();

            IStorageDataLoader<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            return new Storage<TData>(mono, loadersContainer);
        }
    }
}
