using System;

namespace Desdiene.Types.Processes
{
    public partial class OptionalLinearProcess
    {
        private class Completed : State
        {
            public Completed(OptionalLinearProcess it) : base(it) { }

            public override bool KeepWaiting => false;

            public override void Start() { }

            public override void Complete() { }

            public override Action SubscribeToWhenRunning(Action onStarted, Action value)
            {
                value?.Invoke();
                return onStarted += value;
            }

            public override Action SubscribeToWhenCompleted(Action onCompleted, Action value)
            {
                value?.Invoke();
                return onCompleted += value;
            }

            protected override void OnEnter()
            {
                It.WhenCompleted?.Invoke();
            }
        }
    }
}
