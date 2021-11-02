using System;
using System.Linq;
using Desdiene.DataSaving.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Zenject;

public class GameStatisticsStorageAsync : MonoBehaviourExt, IStorageAsync<GameStatisticsDto>
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

        IStorageAsync<GameStatisticsDto>[] storages = new IStorageAsync<GameStatisticsDto>[]
        {
            new JsonCryptoDeviceAsync<GameStatisticsDto>(this, BaseFileName, jsonDeserializer),
            new JsonGooglePlayAsync<GameStatisticsDto>(this, BaseFileName, jsonDeserializer, _gpgsAutentification.Get())
        };
        storages = storages.Select(it => new StorageAsyncLogger<GameStatisticsDto>(it)).ToArray();

        _storage = new StoragesAsyncContainer<GameStatisticsDto>(storages);
    }

    event Action<bool, GameStatisticsDto> IStorageAsync<GameStatisticsDto>.OnReaded
    {
        add => _storage.OnReaded += value;
        remove => _storage.OnReaded -= value;
    }

    event Action<bool> IStorageAsync<GameStatisticsDto>.OnUpdated
    {
        add => _storage.OnUpdated += value;
        remove => _storage.OnUpdated -= value;
    }

    event Action<bool> IStorageAsync<GameStatisticsDto>.OnDeleted
    {
        add => _storage.OnDeleted += value;
        remove => _storage.OnDeleted -= value;
    }

    string IStorageAsync<GameStatisticsDto>.StorageName => _storage.StorageName;

    void IStorageAsync<GameStatisticsDto>.Read() => _storage.Read();

    void IStorageAsync<GameStatisticsDto>.Update(GameStatisticsDto dto) => _storage.Update(dto);

    void IStorageAsync<GameStatisticsDto>.Delete() => _storage.Delete();
}
