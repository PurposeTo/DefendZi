using Desdiene.StateMachines.StateSwitchers;
using Desdiene.TimeControls.Scales.States.Base;
using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scales.States
{
    public class Play : State
    {
        public Play(IStateSwitcher<State> stateSwitcher, IPercent timeScaleRef) : base(stateSwitcher, timeScaleRef) { }

        public override void SetPause(bool pause)
        {
            if (pause)
            {
                SwitchState<Pause>();
            }
        }

        protected override void OnEnter()
        {
            TimeScaleRef.Set(RequiredTimeScaleValue);
        }
    }
}
