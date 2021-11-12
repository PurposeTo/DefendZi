using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls
{
    public sealed class LocalTime : ITime
    {
        private readonly ITime _time;

        public LocalTime()
        {
            IPercent timeScaleRef = new Percent(1f); // По умолчанию время идет
            _time = new TimeSpeed(timeScaleRef);
        }

        event Action ITimeNotificator.WhenStopped
        {
            add => _time.WhenStopped += value;
            remove => _time.WhenStopped -= value;
        }

        event Action ITimeNotificator.WhenRunning
        {
            add => _time.WhenRunning += value;
            remove => _time.WhenRunning -= value;
        }

        event Action ITimeNotificator.OnChanged
        {
            add => _time.OnChanged += value;
            remove => _time.OnChanged -= value;
        }

        float ITimeAccessor.Scale => _time.Scale;
        bool ITimeAccessor.IsPause => _time.IsPause;

        void ITimeMutator.Set(float timeScale) => _time.Set(timeScale);

        IProcess ITimePauseFactory.CreatePause(MonoBehaviourExt mono, string name) => _time.CreatePause(mono, name);
    }
}
