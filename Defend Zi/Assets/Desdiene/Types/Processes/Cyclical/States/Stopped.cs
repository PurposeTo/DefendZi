using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.Cyclical.States
{
    public class Stopped : State
    {
        public Stopped(IStateSwitcher<State> stateSwitcher,
                         StateContext stateContext,
                         string name)
            : base(stateSwitcher,
                   stateContext,
                   name)
        { }

        public override bool KeepWaiting => false;

        public override void Start()
        {
            SwitchState<Running>();
        }

        public override void Stop() { }

        protected override void OnEnter()
        {
            StateContext.OnStopped?.Invoke();
        }

        protected override Action SubscribeToOnStarted(Action onStarted, Action value) => onStarted += value;

        protected override Action SubscribeToOnStopped(Action onCompleted, Action value)
        {
            value?.Invoke();
            return onCompleted += value;
        }
    }
}
