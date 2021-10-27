using Desdiene.AudioPlayers;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviourExt
{
    [SerializeField, NotNull] private AudioClip _audio;
    private IAudioPlayer _audioPlayer;
    private IGameSettingsAccessorNotifier _gameSettings;

    [Inject]
    private void Constructor(GameSettings gameSettings)
    {
        _gameSettings = gameSettings ?? throw new System.ArgumentNullException(nameof(gameSettings));
    }

    protected override void AwakeExt()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        _audioPlayer = new AudioPlayer2D(audioSource);
        SetMuteState();
        _audioPlayer.SetLoop(true);
        _audioPlayer.Play(_audio);

        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _gameSettings.OnSoundMutedChanged += SetMuteState;
    }

    private void UnsubscribeEvents()
    {
        _gameSettings.OnSoundMutedChanged -= SetMuteState;
    }

    private void SetMuteState()
    {
        _audioPlayer.SetMute(_gameSettings.SoundMuted);
    }
}
