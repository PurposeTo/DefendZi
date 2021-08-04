using Desdiene.GameDataAsset;
using Desdiene.GameDataAsset.ConcreteLoaders;
using Desdiene.GameDataAsset.Storage;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;

public class DataStorage : MonoBehaviourExt, IStorage<IGameData>
{
    private const string fileName = "GameData";
    
    private IStorage<GameData> _storage;

    protected override void AwakeExt()
    {
        IJsonConvertor<GameData> jsonConvertor = new NewtonsoftJsonConvertor<GameData>();

        var deviceLoader = new DeviceJsonDataLoader<GameData>(this, fileName, jsonConvertor);
        _storage = DataAssetIniter<GameData>.GetStorage(this, new DataCombiner(), deviceLoader);
        // Загрузка даты инициализируется сразу после создания данного класса.
        _storage.InvokeLoadingData();
    }

    public IGameData GetData() => _storage.GetData();

    public void InvokeLoadingData() => _storage.InvokeLoadingData();

    public void InvokeSavingData() => _storage.InvokeSavingData();
}
