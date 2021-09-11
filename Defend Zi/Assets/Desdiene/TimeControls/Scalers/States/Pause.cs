using Desdiene.StateMachines.StateSwitchers;
using Desdiene.TimeControls.Scales.States.Base;
using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scales.States
{
    public class Pause : State
    {
        public Pause(IStateSwitcher<State> stateSwitcher, IPercent timeScaleRef) : base(stateSwitcher, timeScaleRef) { }

        public override void SetPause(bool pause)
        {
            if (!pause)
            {
                SwitchState<Play>();
            }
        }

        protected override void OnEnter()
        {
            TimeScaleRef.Set(0);
        }
    }
}
