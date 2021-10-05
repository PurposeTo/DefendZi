using UnityEngine;

namespace Desdiene.AudioPlayers
{
    public interface IAudioPlayer
    {
        void Mute();
        void UnMute();
        void SetVolume(float volume);
        void Play(AudioClip audio);
        void SetLoop(bool loop);
        void Stop();
        void Pause();
        void UnPause();
    }
}
