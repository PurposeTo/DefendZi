using System;
using Desdiene.StateMachines.States;

public partial class HealthReincarnation
{
    private abstract class State : IStateEntryExitPoint
    {
        protected State(HealthReincarnation it)
        {
            It = it ?? throw new ArgumentNullException(nameof(it));
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

        protected State SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
    }
}
