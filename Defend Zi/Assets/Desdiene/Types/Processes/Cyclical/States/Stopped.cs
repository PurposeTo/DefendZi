using System;

namespace Desdiene.Types.Processes
{
    public partial class CyclicalProcess
    {
        private class Stopped : State
        {
            public Stopped(CyclicalProcess it) : base(it) { }

            public override bool KeepWaiting => false;

            public override void Start()
            {
                SwitchState<Running>();
            }

            public override void Stop() { }

            protected override void OnEnter()
            {
                It.WhenCompleted?.Invoke();
            }

            public override Action SubscribeToWhenRunning(Action onStarted, Action value) => onStarted += value;

            public override Action SubscribeToWhenCompleted(Action onCompleted, Action value)
            {
                value?.Invoke();
                return onCompleted += value;
            }
        }
    }
}
