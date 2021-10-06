using Desdiene.AudioPlayers;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviourExt
{
    [SerializeField, NotNull] private AudioClip _audio;
    private IAudioPlayer _audioPlayer;

    protected override void AwakeExt()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        _audioPlayer = new AudioPlayer2D(audioSource);
        _audioPlayer.SetLoop(true);
        _audioPlayer.Play(_audio);
    }
}
