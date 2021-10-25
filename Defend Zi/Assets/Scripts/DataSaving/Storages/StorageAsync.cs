using System;
using Desdiene.DataSaving.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Zenject;

public class StorageAsync : MonoBehaviourExt, IStorageAsync<GameStatisticsDto>
{
    private const string BaseFileName = "GameData";
    private IStorageAsync<GameStatisticsDto> _storage;
    private GpgsAutentification _gpgsAutentification;

    [Inject]
    private void Constructor(GpgsAutentification gpgsAutentification)
    {
        _gpgsAutentification = gpgsAutentification ?? throw new ArgumentNullException(nameof(gpgsAutentification));
    }

    protected override void AwakeExt()
    {
        var jsonDeserializer = new GameStatisticsDtoJsonConvertor();
        
        var deviceStorage = new JsonDeviceAsync<GameStatisticsDto>(this, BaseFileName, jsonDeserializer);
        var googlePlayStorage = new JsonGooglePlayAsync<GameStatisticsDto>(this, BaseFileName, jsonDeserializer, _gpgsAutentification.Get());
        _storage = new StoragesAsyncContainer<GameStatisticsDto>(deviceStorage, googlePlayStorage);
    }

    string IStorageAsync<GameStatisticsDto>.StorageName => _storage.StorageName;

    void IStorageAsync<GameStatisticsDto>.Delete(Action<bool> successResult) => _storage.Delete(successResult);

    void IStorageAsync<GameStatisticsDto>.Read(Action<bool, GameStatisticsDto> result) => _storage.Read(result);

    void IStorageAsync<GameStatisticsDto>.Update(GameStatisticsDto data, Action<bool> successResult) => _storage.Update(data, successResult);
}
