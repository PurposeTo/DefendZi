using System.Linq;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.DataLoader.Safe;
using Desdiene.GameDataAsset.DataLoader.Storage;
using Desdiene.GameDataAsset.DataSynchronizer;
using Desdiene.MonoBehaviourExtention;

namespace Desdiene.GameDataAsset
{
    public class DataAssetInitializer<TData, TGetter, TSetter, TChangingNotifier>
        where TGetter : IDataGetter
        where TData : GameData, TGetter, new()
        where TSetter : DataSetter<TData>, IDataSetter, new()
        where TChangingNotifier : IDataChangingNotifier
    {
        public readonly Model.DataModel<TData, TGetter, TSetter, TChangingNotifier> dataModel;
        public readonly Synchronizer<TData> synchronizer;

        public DataAssetInitializer(MonoBehaviourExt superMonoBehaviour, params StorageJsonDataLoader<TData>[] storages)
        {
            dataModel = new Model.DataModel<TData, TGetter, TSetter, TChangingNotifier>();

            IStorageDataLoader<TData>[] safeReaderWriters = storages
                .Select(storage => new SafeStorageDataLoader<TData>(storage))
                .ToArray();

            IStorageDataLoader<TData> loadersContainer = new LoadersContainer<TData>(safeReaderWriters);
            synchronizer = new Synchronizer<TData>(superMonoBehaviour, dataModel, loadersContainer);
        }
    }
}
