using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls
{
    public sealed class Time : ITime
    {
        private readonly IPercent _timeScaleRef;
        private readonly IPercent _requiredTimeScale;
        private readonly IProcesses _pauses;
        private readonly IProcess _scalePause;

        float ITimeAccessor.Scale => _timeScaleRef.Value;

        public Time(IPercent timeScaleRef)
        {
            _timeScaleRef = timeScaleRef ?? throw new ArgumentNullException(nameof(timeScaleRef));
            _requiredTimeScale = new Percent(_timeScaleRef.Value);
            _pauses = new ParallelProcesses("Остановка времени");
            _scalePause = new CyclicalProcess("Scale времени равно 0");
            _pauses.Add(_scalePause);

            SubscribeEvents();
        }

        // когда время идет, пауза остановлена и наоборот.

        event Action ITimeNotificator.WhenStopped
        {
            add => _pauses.WhenRunning += value;
            remove => _pauses.WhenRunning -= value;
        }

        event Action ITimeNotificator.WhenRunning
        {
            add => _pauses.WhenCompleted += value;
            remove => _pauses.WhenCompleted -= value;
        }

        event Action ITimeNotificator.OnChanged
        {
            add => _timeScaleRef.OnChanged += value;
            remove => _timeScaleRef.OnChanged -= value;
        }

        void ITimeMutator.Set(float timeScale) => _requiredTimeScale.Set(timeScale);

        private void SubscribeEvents()
        {
            _requiredTimeScale.OnChanged += () =>
            {
                // если time scale равен нулю, то это считается постановкой времени на паузу.
                if (_requiredTimeScale.IsMin) _scalePause.Start();
                else
                {
                    _scalePause.Stop();
                    SetRequiredScaleToActual();
                }
            };

            _pauses.WhenCompleted += SetRequiredScaleToActual;
            _pauses.WhenRunning += SetZeroActualScale;
        }

        IProcess ITimePauseFactory.CreatePause(MonoBehaviourExt mono, string name)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            IProcess pause = new CyclicalProcess(name);
            _pauses.Add(pause);

            void RemoveAndDescribe()
            {
                _pauses.Remove(pause);
                mono.OnDestroyed -= RemoveAndDescribe;
            }
            mono.OnDestroyed += RemoveAndDescribe;
            return pause;
        }

        private void SetRequiredScaleToActual() => _timeScaleRef.Set(_requiredTimeScale.Value);

        private void SetZeroActualScale() => _timeScaleRef.Set(0);
    }
}
