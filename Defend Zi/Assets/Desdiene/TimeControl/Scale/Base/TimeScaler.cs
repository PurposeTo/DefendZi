using System;
using Desdiene.Types.PercentAsset;

namespace Desdiene.TimeControl.Scale.Base
{
    public abstract class TimeScaler : ITimeScaler
    {
        private readonly IPercent timeScaleSaved;  // Сохраненное значение скорости времени

        public TimeScaler()
        {
            timeScaleSaved = new Percent(TimeScale);
        }

        public abstract float TimeScale { get; protected set; }

        public bool IsPause { get; private set; } = false; // По умолчанию паузы нет

        public event Action<float> OnTimeScaleChanged;

        public void SetPause(bool pause)
        {
            IsPause = pause;
            SetTimeScaleViaPause();
        }

        public void SetTimeScale(float timeScale)
        {
            timeScaleSaved.Set(timeScale);
        }

        private void SetTimeScaleViaPause()
        {
            if (IsPause) TimeScale = 0f;
            else TimeScale = timeScaleSaved.Value;
            OnTimeScaleChanged?.Invoke(TimeScale);
        }
    }
}
