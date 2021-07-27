using Desdiene.GameDataAsset;
using Desdiene.GameDataAsset.ConcreteLoaders;
using Desdiene.GameDataAsset.Storage;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;

public class DataStorage : MonoBehaviourExt, IStorage<IGameData>
{
    private const string fileName = "GameData";

    private IStorage<GameData> storage;


    protected override void AwakeExt()
    {
        IJsonConvertor<GameData> jsonConvertor = new NewtonsoftJsonConvertor<GameData>();

        var deviceLoader = new DeviceJsonDataLoader<GameData>(this, fileName, jsonConvertor);
        storage = DataAssetIniter<GameData>.GetStorage(this, deviceLoader);
        storage.InvokeLoadingData();
    }

    public IGameData GetData() => storage.GetData();

    public void InvokeLoadingData() => storage.InvokeLoadingData();

    public void InvokeSavingData() => storage.InvokeSavingData();
}
