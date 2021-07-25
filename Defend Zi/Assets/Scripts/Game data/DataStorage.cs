using Desdiene.GameDataAsset;
using Desdiene.GameDataAsset.ConcreteLoaders;
using Desdiene.GameDataAsset.Storage;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class DataStorage : MonoBehaviourExt, IStorage<IGameData>
{
    private const string fileName = "GameData";

    private IStorage<GameData> storage;


    protected override void AwakeExt()
    {
        //IJsonConvertor<GameData> jsonConvertor = new NewtonsoftJsonConvertor<GameData>();

        //var deviceLoader = new DeviceJsonDataLoader<GameData>(this, fileName, jsonConvertor);
        //storage = DataAssetIniter<GameData>.GetStorage(this, deviceLoader);
    }

    public IGameData GetData() => storage.GetData();

    public void LoadFromStorage() => storage.LoadFromStorage();

    public void SaveToStorage() => storage.SaveToStorage();
}
