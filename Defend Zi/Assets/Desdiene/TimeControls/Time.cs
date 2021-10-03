using System;
using Desdiene.Types.Percents;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls
{
    public sealed class Time : ITime
    {
        private readonly IPercent _timeScaleRef;
        private readonly IPercent _requiredTimeScale;
        private readonly ICyclicalProcess _pause;

        public Time(IPercent timeScaleRef)
        {
            _timeScaleRef = timeScaleRef ?? throw new ArgumentNullException(nameof(timeScaleRef));
            _requiredTimeScale = new Percent(1f);
            _pause = new CyclicalProcess("Остановка времени");

            SubscribeEvents();
        }

        // когда время идет, пауза остановлена и наоборот.
        void ITime.Stop() => _pause.Start();
        void ITime.Run() => _pause.Stop();

        void ITime.Set(float timeScale) => _requiredTimeScale.Set(timeScale);

        private void SubscribeEvents()
        {
            _pause.WhenStopped += () => _timeScaleRef.Set(_requiredTimeScale.Value);
            _pause.WhenStarted += () => _timeScaleRef.Set(0);
        }
    }
}
