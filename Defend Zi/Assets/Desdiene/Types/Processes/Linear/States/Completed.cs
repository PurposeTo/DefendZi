using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.Linear.States
{
    public class Completed : State
    {
        public Completed(IStateSwitcher<State> stateSwitcher,
                         StateContext stateContext,
                         string name)
            : base(stateSwitcher,
                   stateContext,
                   name)
        { }

        public override bool KeepWaiting => false;

        public override void Start() { }

        public override void Complete() { }

        protected override void OnEnter()
        {
            StateContext.OnCompleted?.Invoke();
        }

        protected override Action SubscribeToOnStarted(Action onStarted, Action value)
        {
            value?.Invoke();
            return onStarted += value;
        }

        protected override Action SubscribeToOnCompleted(Action onCompleted, Action value)
        {
            value?.Invoke();
            return onCompleted += value;
        }
    }
}
