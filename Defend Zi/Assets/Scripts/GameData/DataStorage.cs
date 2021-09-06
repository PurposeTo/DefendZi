using Desdiene.DataStorageFactories;
using Desdiene.DataStorageFactories.ConcreteLoaders;
using Desdiene.DataStorageFactories.Storages;
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
        _storage = StorageFactory<GameData>.GetStorage(this, deviceLoader);
        // Загрузка даты инициализируется сразу после создания данного класса.
        InvokeLoadingData();
    }

    public IGameData GetData() => _storage.GetData();

    public void InvokeLoadingData() => _storage.InvokeLoadingData();

    public void InvokeSavingData() => _storage.InvokeSavingData();
}
