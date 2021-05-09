namespace Desdiene.TimeControl.Scale.Base
{
    public abstract class TimeScaler
    {
        private readonly ValueClampable timeScaleSaved;  // Сохраненное значение скорости времени

        public TimeScaler()
        {
            timeScaleSaved = new ValueClampable(TimeScale, 0f, 1f);
        }

        public abstract float TimeScale { get; protected set; }

        public bool IsPause { get; private set; } = false; // По умолчанию паузы нет

        public void SetPause(bool pause)
        {
            IsPause = pause;
            SetTimeScaleViaPause();
        }

        public void SetTimeScale(float timeScale)
        {
            timeScaleSaved.SetClamped(timeScale);
        }

        public void SetTimeScaleUnclaimedMax(float timeScale)
        {
            timeScaleSaved.SetUnclaimedMax(timeScale);
        }

        private void SetTimeScaleViaPause()
        {
            if (IsPause) TimeScale = 0f;
            else TimeScale = timeScaleSaved.Value;
        }
    }
}
