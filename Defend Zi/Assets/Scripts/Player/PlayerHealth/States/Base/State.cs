using System;
using Desdiene.StateMachines.States;
using Desdiene.MonoBehaviourExtension;

public partial class PlayerHealth
{
    private abstract class State : MonoBehaviourExtContainer, IState
    {
       private readonly string _name;

        protected State(MonoBehaviourExt mono, PlayerHealth it) : base(mono)
        {
            It = it ?? throw new ArgumentNullException(nameof(it));
            _name = GetType().Name;
        }

        string IState.Name => _name;

        void IState.OnEnter()
        {
            OnEnter();
        }

        void IState.OnExit()
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
