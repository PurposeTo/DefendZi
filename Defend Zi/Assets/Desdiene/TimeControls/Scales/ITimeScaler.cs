using System;

namespace Desdiene.TimeControls.Scales
{
    public interface ITimeScaler
    {
        event Action<float> OnTimeScaleChanged;
        float TimeScale { get; }

        bool IsPause { get; }

        void SetPause(bool pause);

        void SetTimeScale(float timeScale);
    }
}
