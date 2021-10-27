public interface IGameSettings : IGameSettingsAccessorNotifier, IGameSettingsMutator
{
    void Save();
}
