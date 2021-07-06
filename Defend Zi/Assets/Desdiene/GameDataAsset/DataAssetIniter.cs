using System.Linq;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Safe;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.GameDataAsset.DataSynchronizer;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.GameDataAsset
{
    public static class DataAssetIniter<TData> where TData : IData, new()
    {
        public static IStorage<TData> GetStorage(MonoBehaviourExt superMonoBehaviour, params StorageJsonDataLoader<TData>[] storages)
        {
            IStorageDataLoader<TData>[] safeReaderWriters = storages
                .Select(storage => new SafeStorageDataLoader<TData>(storage))
                .ToArray();

            IStorageDataLoader<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            return new Storage<TData>(superMonoBehaviour, loadersContainer);
        }
    }
}
