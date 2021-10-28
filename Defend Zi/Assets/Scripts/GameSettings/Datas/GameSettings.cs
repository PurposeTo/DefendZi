using System;
using Desdiene.DataSaving.Datas;
using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Статистика игры.
/// Класс сделан MonoBehaviour для возможности чтения полей через инспектор.
/// </summary>
public class GameSettings : MonoBehaviourExt, IGameSettings
{
    private IStorage<GameSettingsDto> _storage;
    // todo добавить события об изменении
    private bool _soundMuted;

    [Inject]
    private void Constructor(IStorage<GameSettingsDto> storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    protected override void AwakeExt()
    {
        if (_storage.TryToRead(out GameSettingsDto dto))
        {
            UpdateDataFromDto(dto);
        }
    }

    protected void OnDestroy()
    {
        Save();
    }

    private event Action OnSoundEnabledChanged;

    event Action IGameSettingsNotifier.OnSoundMutedChanged
    {
        add => OnSoundEnabledChanged += value;
        remove => OnSoundEnabledChanged -= value;
    }

    bool IGameSettingsAccessor.SoundMuted => _soundMuted;
    void IGameSettingsMutator.SetMuteState(bool mute) => SetMuteState(mute);
    void ISavableData.Save() => Save();

    private void Save()
    {
        var dto = new GameSettingsDto()
        {
            SoundMuted = _soundMuted
        };

        _storage.TryToUpdate(dto);
    }

    private void SetMuteState(bool mute)
    {
        if (_soundMuted != mute)
        {
            _soundMuted = mute;
            OnSoundEnabledChanged?.Invoke();
        }
    }

    private void UpdateDataFromDto(GameSettingsDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto)); // dto не может быть null, если success == true

        SetMuteState(dto.SoundMuted);
    }
}
