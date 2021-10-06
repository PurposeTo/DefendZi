using Desdiene.AudioPlayers;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviourExt
{
    private AudioClip _audio; 
    private IAudioPlayer _audioPlayer;

    protected override void AwakeExt()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        _audio = Resources.Load<AudioClip>("Audio/Bio Unit - Deep");

        _audioPlayer = new AudioPlayer2D(audioSource);
        _audioPlayer.SetLoop(true);
        _audioPlayer.Play(_audio);
    }
}
