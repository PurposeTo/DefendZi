using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.States
{
    public class Created : State
    {
        public Created(IStateSwitcher<State> stateSwitcher,
                       StateContext stateContext,
                       string name)
            : base(stateSwitcher,
                   stateContext,
                   name)
        { }

        public override void Start()
        {
            SwitchState<Running>();
        }

        public override void Complete()
        {
            SwitchState<Running>().Complete();
        }

        protected override Action SubscribeToOnStarted(Action onStarted, Action value) => onStarted += value;

        protected override Action SubscribeToOnCompleted(Action onCompleted, Action value) => onCompleted += value;
    }
}
