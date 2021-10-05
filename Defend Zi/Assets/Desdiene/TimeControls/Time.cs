﻿using System;
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
        private readonly ICyclicalProcesses _pauses;
        private readonly ICyclicalProcess _scalePause;

        float ITimeAccessor.Scale => _timeScaleRef.Value;

        public Time(IPercent timeScaleRef)
        {
            _timeScaleRef = timeScaleRef ?? throw new ArgumentNullException(nameof(timeScaleRef));
            _requiredTimeScale = new Percent(_timeScaleRef.Value);
            _pauses = new CyclicalParallelProcesses("Остановка времени");
            _scalePause = new CyclicalProcess("Scale времени равно 0");
            _pauses.Add(_scalePause);

            SubscribeEvents();
        }

        // когда время идет, пауза остановлена и наоборот.

        event Action ITimeNotification.WhenStopped
        {
            add => _pauses.WhenStarted += value;
            remove => _pauses.WhenStarted -= value;
        }

        event Action ITimeNotification.WhenRunning
        {
            add => _pauses.WhenStopped += value;
            remove => _pauses.WhenStopped -= value;
        }

        event Action ITimeNotification.OnChanged
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

            _pauses.WhenStopped += SetRequiredScaleToActual;
            _pauses.WhenStarted += SetZeroActualScale;
        }

        ICyclicalProcess ITimePauseFactory.CreatePause(MonoBehaviourExt mono, string name)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            ICyclicalProcess pause = new CyclicalProcess(name);
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