using System;

public interface IGameSettingsNotifier
{
    event Action OnSoundMutedChanged;
}
