using System;
using UnityEngine;

namespace Desdiene.AudioPlayers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer2D : IAudioPlayer
    {
        private readonly AudioSource _audioSource;

        public AudioPlayer2D(AudioSource audioSource)
        {
            _audioSource = audioSource ?? throw new ArgumentNullException(nameof(audioSource));
            _audioSource.spatialBlend = 0f;
        }

        void IAudioPlayer.SetMute(bool mute) => _audioSource.mute = mute;
        void IAudioPlayer.Mute() => _audioSource.mute = true;
        void IAudioPlayer.UnMute() => _audioSource.mute = false;

        void IAudioPlayer.Play(AudioClip audio)
        {
            if (audio is null) throw new ArgumentNullException(nameof(audio));

            Stop();
            _audioSource.clip = audio;
            _audioSource.Play();
        }

        void IAudioPlayer.SetLoop(bool loop) => _audioSource.loop = loop;

        void IAudioPlayer.SetVolume(float volume) => _audioSource.volume = volume;

        void IAudioPlayer.Stop() => Stop();

        void IAudioPlayer.Pause() => _audioSource.Pause();

        void IAudioPlayer.UnPause() => _audioSource.UnPause();

        private void Stop()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }
}
