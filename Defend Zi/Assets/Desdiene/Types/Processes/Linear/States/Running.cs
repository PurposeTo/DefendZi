using System;

namespace Desdiene.Types.Processes
{
    public partial class OptionalLinearProcess
    {
        private class Running : State
        {
            public Running(OptionalLinearProcess it) : base(it) { }

            public override bool KeepWaiting => true;

            public override void Start() { }

            public override void Complete()
            {
                SwitchState<Completed>();
            }

            public override Action SubscribeToWhenRunning(Action action, Action value)
            {
                value?.Invoke();
                return action += value;
            }

            public override Action SubscribeToWhenCompleted(Action onCompleted, Action value) => onCompleted += value;

            protected override void OnEnter()
            {
                It.WhenRunning?.Invoke();
            }

            protected override void OnExit()
            {
                base.OnExit();
            }
        }
    }
}
