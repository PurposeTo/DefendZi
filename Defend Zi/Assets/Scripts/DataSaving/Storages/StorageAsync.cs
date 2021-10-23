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

    void IStorageAsync<SavableDataAsync>.Delete(Action<bool> successResult) => _storage.Delete(successResult);

    void IStorageAsync<SavableDataAsync>.Read(Action<bool, SavableDataAsync> result) => _storage.Read(result);

    void IStorageAsync<SavableDataAsync>.Update(SavableDataAsync data, Action<bool> successResult) => _storage.Update(data, successResult);
}
