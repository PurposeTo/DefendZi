using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.TimeControls.Scalers;
using Desdiene.TimeControls.Scales.States;
using Desdiene.TimeControls.Scales.States.Base;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scales
{
    public sealed class TimeScaler : IManualTimeController
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public TimeScaler(IPercent timeScaleRef)
        {
            StateSwitcher<State> stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Play(stateSwitcher, timeScaleRef),
                new Pause(stateSwitcher, timeScaleRef)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Play>();
        }

        private State CurrentState => _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));

        void ITimePauser.SetPause(bool pause) => CurrentState.SetPause(pause);

        void ITimeScaler.SetScale(float timeScale) => CurrentState.SetScale(timeScale);
    }
}
