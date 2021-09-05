using System;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes.States.Base;

namespace Desdiene.Types.Processes.States
{
    public class Created : State
    {
        public Created(IStateSwitcher<State, StateContext> stateSwitcher, string name) : base(stateSwitcher, name) { }

        public override void Start()
        {
            SwitchState<Running>();
        }

        public override void Complete()
        {
            SwitchState<Running>().Complete();
        }

        protected override void SubscribeToOnStarted(Action value) => onStarted += value;

        protected override void SubscribeToOnCompleted(Action value) => onCompleted += value;
    }
}
