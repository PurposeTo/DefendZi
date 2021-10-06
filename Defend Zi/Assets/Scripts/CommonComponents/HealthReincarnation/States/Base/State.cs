using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

public partial class HealthReincarnation
{
    private abstract class State : IStateEntryExitPoint<HealthReincarnation>
    {
        private readonly IStateSwitcher<State, HealthReincarnation> _stateSwitcher;
        private readonly HealthReincarnation _it;

        protected State(HealthReincarnation it, IStateSwitcher<State, HealthReincarnation> stateSwitcher)
        {
            _it = it ?? throw new ArgumentNullException(nameof(it));
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
        }

        void IStateEntryExitPoint<HealthReincarnation>.OnEnter(HealthReincarnation it)
        {
            OnEnter(it);
        }

        void IStateEntryExitPoint<HealthReincarnation>.OnExit(HealthReincarnation it)
        {
            OnExit(it);
        }

        public void TakeDamage(IDamage damage) => TakeDamage(_it, damage);
        public void Revive() => Revive(_it);

        protected abstract void TakeDamage(HealthReincarnation it, IDamage damage);
        protected abstract void Revive(HealthReincarnation it);

        public abstract Action SubscribeToWhenAlive(Action action, Action value);
        public abstract Action SubscribeToWhenDead(Action action, Action value);

        protected virtual void OnEnter(HealthReincarnation it) { }
        protected virtual void OnExit(HealthReincarnation it) { }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
