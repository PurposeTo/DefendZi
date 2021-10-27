using System;
using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

/// <summary>
/// Статистика игры.
/// Класс сделан MonoBehaviour для возможности чтения полей через инспектор.
/// </summary>
public class GameSettings : MonoBehaviourExt, IGameSettingsAccessorNotifier
{
    private IStorage<GameSettingsDto> _storage;
    // todo добавить события об изменении
    private bool _soundEnabled;


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

    bool IGameSettingsAccessorNotifier.SoundEnabled => _soundEnabled;

    public void Save()
    {
        var dto = new GameSettingsDto()
        {
            SoundEnabled = _soundEnabled
        };

        _storage.Update(dto);
    }

    public void SetSoundEnabled(bool enabled)
    {
        _soundEnabled = enabled;
    }

    private void UpdateDataFromDto(GameSettingsDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto)); // dto не может быть null, если success == true

        SetSoundEnabled(dto.SoundEnabled);
    }
}
