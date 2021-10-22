using System;
using Desdiene.DataSaving.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Zenject;

public class StorageAsync : MonoBehaviourExt, IStorageAsync<SavableDataAsync>
{
    private const string BaseFileName = "GameData";
    private IStorageAsync<SavableDataAsync> _storage;
    private GpgsAutentification _gpgsAutentification;

    [Inject]
    private void Constructor(GpgsAutentification gpgsAutentification)
    {
        _gpgsAutentification = gpgsAutentification ?? throw new ArgumentNullException(nameof(gpgsAutentification));
    }

    protected override void AwakeExt()
    {
        var jsonDeserializer = new DataAsyncJsonConvertor();
        
        var deviceStorage = new JsonDeviceAsync<SavableDataAsync>(this, BaseFileName, jsonDeserializer);
        var googlePlayStorage = new JsonGooglePlayAsync<SavableDataAsync>(this, BaseFileName, jsonDeserializer, _gpgsAutentification.Get());
        _storage = new StoragesAsyncContainer<SavableDataAsync>(deviceStorage, googlePlayStorage);
    }

    string IStorageAsync<SavableDataAsync>.StorageName => _storage.StorageName;

    void IStorageAsync<SavableDataAsync>.Clean(Action<bool> successResult) => _storage.Clean(successResult);

    void IStorageAsync<SavableDataAsync>.Load(Action<bool, SavableDataAsync> result) => _storage.Load(result);

    void IStorageAsync<SavableDataAsync>.Save(SavableDataAsync data, Action<bool> successResult) => _storage.Save(data, successResult);
}
