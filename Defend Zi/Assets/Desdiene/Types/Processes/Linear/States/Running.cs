using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.Linear.States
{
    public class Running : State
    {
        public Running(IStateSwitcher<State> stateSwitcher,
                       StateContext stateContext,
                       string name)
            : base(stateSwitcher,
                   stateContext,
                   name)
        { }

        public override bool KeepWaiting => true;

        public override void Start() { }

        public override void Complete()
        {
            SwitchState<Completed>();
        }

        protected override void OnEnter()
        {
            StateContext.OnStarted?.Invoke();
        }

        protected override Action SubscribeToOnStarted(Action onStarted, Action value)
        {
            value?.Invoke();
            return onStarted += value;
        }

        protected override Action SubscribeToOnCompleted(Action onCompleted, Action value) => onCompleted += value;
    }
}
