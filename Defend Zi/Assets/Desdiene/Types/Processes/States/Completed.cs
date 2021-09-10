using System;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.States
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

        public override void Start()
        {
            SwitchState<Created>().Start();
        }

        public override void Complete() { }

        protected override void OnEnter()
        {
            onCompleted?.Invoke();
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
