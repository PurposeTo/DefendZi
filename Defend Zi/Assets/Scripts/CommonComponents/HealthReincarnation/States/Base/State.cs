using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

public partial class HealthReincarnation
{
    private abstract class State : IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;

        protected State(HealthReincarnation it, IStateSwitcher<State> stateSwitcher)
        {
            It = it ?? throw new ArgumentNullException(nameof(it));
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
        }

        void IStateEntryExitPoint.OnEnter()
        {
            OnEnter();
        }

        void IStateEntryExitPoint.OnExit()
        {
            OnExit();
        }

        protected HealthReincarnation It { get; }

        public abstract void TakeDamage(IDamage damage);
        public abstract void Revive();

        public abstract Action SubscribeToWhenAlive(Action action, Action value);
        public abstract Action SubscribeToWhenDead(Action action, Action value);

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
