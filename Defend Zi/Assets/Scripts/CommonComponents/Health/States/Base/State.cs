using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

public partial class PlayerHealth
{
    private abstract class State : IStateEntryExitPoint<PlayerHealth>
    {
        private readonly IStateSwitcher<State, PlayerHealth> _stateSwitcher;
        private readonly PlayerHealth _it;

        protected State(PlayerHealth it, IStateSwitcher<State, PlayerHealth> stateSwitcher)
        {
            _it = it ?? throw new ArgumentNullException(nameof(it));
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
        }

        void IStateEntryExitPoint<PlayerHealth>.OnEnter(PlayerHealth it)
        {
            OnEnter(it);
        }

        void IStateEntryExitPoint<PlayerHealth>.OnExit(PlayerHealth it)
        {
            OnExit(it);
        }

        public void TakeDamage(IDamage damage) => TakeDamage(_it, damage);
        public void Revive() => Revive(_it);

        protected abstract void TakeDamage(PlayerHealth it, IDamage damage);
        protected abstract void Revive(PlayerHealth it);

        public abstract Action SubscribeToWhenAlive(Action action, Action value);
        public abstract Action SubscribeToWhenDead(Action action, Action value);

        protected virtual void OnEnter(PlayerHealth it) { }
        protected virtual void OnExit(PlayerHealth it) { }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
