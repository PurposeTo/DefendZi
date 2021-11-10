using System;
using Desdiene.StateMachines.States;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Containers;

public partial class PlayerHealth
{
    private abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
    {
        protected State(MonoBehaviourExt mono, PlayerHealth it) : base(mono)
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

        protected PlayerHealth It { get; }

        public abstract void TakeDamage(IDamage damage);
        public abstract void Revive();

        public abstract Action SubscribeToWhenAlive(Action action, Action value);
        public abstract Action SubscribeToWhenDead(Action action, Action value);
        public abstract Action SubscribeToWhenInvulnerable(Action action, Action value);
        public abstract Action SubscribeToWhenMortal(Action action, Action value);

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        protected State SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
    }
}
