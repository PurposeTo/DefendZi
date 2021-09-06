using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.States
{
    public class Running : State
    {
        public Running(IStateSwitcher<State, StateContext> stateSwitcher, string name) : base(stateSwitcher, name) { }

        public override void Start() { }

        public override void Complete()
        {
            SwitchState<Completed>();
        }

        protected override void OnEnter()
        {
            onStarted?.Invoke();
        }

        protected override void SubscribeToOnStarted(Action action) => action?.Invoke();

        protected override void SubscribeToOnCompleted(Action action) => onCompleted += action;
    }
}
