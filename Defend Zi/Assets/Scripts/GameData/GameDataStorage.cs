using System;
using Desdiene.DataStorageFactories;
using Desdiene.DataStorageFactories.ConcreteLoaders;
using Desdiene.DataStorageFactories.Storages.Json;
using Desdiene.DataStorageFactories.DataContainers;
using Desdiene.GooglePlayApi;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using Newtonsoft.Json;
using Zenject;

public class GameDataStorage : MonoBehaviourExt, IDataContainer<IGameData>
{
    private const string fileName = "GameData";

    private PlayGamesPlatform _platform;
    private IDataContainer<GameData> _storage;

    [Inject]
    private void Constructor(GpgsAutentification gpgsAutentification)
    {
        if (gpgsAutentification == null) throw new ArgumentNullException(nameof(gpgsAutentification));
        _platform = gpgsAutentification.Get();
    }

    protected override void AwakeExt()
    {
        var serializeSettings = new JsonSerializerSettings(); // todo добавить safeint/float
        IJsonConvertor<GameData> jsonConvertor = new NewtonsoftJsonConvertor<GameData>(serializeSettings);

        var deviceLoader = new DeviceJsonCryptedData<GameData>(this, fileName, jsonConvertor);
        var googlePlayLoader = new GooglePlayJsonData<GameData>(this, fileName, jsonConvertor, _platform);
        _storage = StorageFactory<GameData>.GetStorage(this, deviceLoader, googlePlayLoader);
        // Загрузка даты инициализируется сразу после создания данного класса.
        InvokeLoadingData();
    }

    public IGameData GetData() => _storage.GetData();

    public void InvokeLoadingData() => _storage.InvokeLoadingData();

    public void InvokeSavingData() => _storage.InvokeSavingData();
}
