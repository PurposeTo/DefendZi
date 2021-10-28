using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;

public class GameSettingsStorage : MonoBehaviourExt, IStorage<GameSettingsDto>
{
    private const string BaseFileName = "GameSettings";
    private IStorage<GameSettingsDto> _storage;

    protected override void AwakeExt()
    {
        var jsonDeserializer = new GameSettingsDtoJsonConvertor();

        _storage = new PlayerPrefsJsonStorage<GameSettingsDto>(BaseFileName, jsonDeserializer);
    }

    string IStorage<GameSettingsDto>.StorageName => _storage.StorageName;

    bool IStorage<GameSettingsDto>.TryToDelete() => _storage.TryToDelete();

    bool IStorage<GameSettingsDto>.TryToRead(out GameSettingsDto data) => _storage.TryToRead(out data);

    bool IStorage<GameSettingsDto>.TryToUpdate(GameSettingsDto data) => _storage.TryToUpdate(data);
}
