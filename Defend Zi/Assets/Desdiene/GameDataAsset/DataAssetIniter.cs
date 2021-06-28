using System.Linq;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Safe;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.GameDataAsset.DataSynchronizer;
using Desdiene.GameDataAsset.Model;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.GameDataAsset
{
    public class DataAssetIniter<TData> where TData : IData, new()
    {
        public readonly DataModel<TData> dataModel;
        public readonly Synchronizer<TData> synchronizer;

        public DataAssetIniter(MonoBehaviourExt superMonoBehaviour, params StorageJsonDataLoader<TData>[] storages)
        {
            dataModel = new DataModel<TData>();

            IStorageDataLoader<TData>[] safeReaderWriters = storages
                .Select(storage => new SafeStorageDataLoader<TData>(storage))
                .ToArray();

            IStorageDataLoader<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            synchronizer = new Synchronizer<TData>(superMonoBehaviour, dataModel, loadersContainer);
        }
    }
}
