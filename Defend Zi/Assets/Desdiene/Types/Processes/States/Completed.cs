using System;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes.States.Base;

namespace Desdiene.Types.Processes.States
{
    public class Completed : State
    {
        public Completed(IStateSwitcher<State, StateContext> stateSwitcher, string name) : base(stateSwitcher, name) { }

        public override void Start()
        {
            SwitchState<Created>().Start();
        }

        public override void Complete() { }

        protected override void OnEnter()
        {
            onCompleted?.Invoke();
        }

        protected override void SubscribeToOnStarted(Action value) => value?.Invoke();

        protected override void SubscribeToOnCompleted(Action value) => value?.Invoke();
    }
}
