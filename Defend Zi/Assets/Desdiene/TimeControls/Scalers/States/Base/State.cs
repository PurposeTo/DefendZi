using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scales.States.Base
{
    public abstract class State : IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly IPercent _requiredTimeScale;

        public State(IStateSwitcher<State> stateSwitcher, IPercent timeScaleRef)
        {
            _stateSwitcher = stateSwitcher ?? throw new System.ArgumentNullException(nameof(stateSwitcher));
            TimeScaleRef = timeScaleRef ?? throw new System.ArgumentNullException(nameof(timeScaleRef));
            _requiredTimeScale = new Percent(1f);
        }

        void IStateEntryExitPoint.OnEnter()
        {
            OnEnter();
        }

        void IStateEntryExitPoint.OnExit()
        {
            OnExit();
        }

        protected float RequiredTimeScaleValue => _requiredTimeScale.Value;
        protected IPercent TimeScaleRef { get; }

        public abstract void SetPause(bool pause);

        public void SetScale(float timeScale)
        {
            _requiredTimeScale.Set(timeScale);
        }

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
