using System;

namespace Desdiene.Types.Processes
{
    public partial class CyclicalProcess
    {
        private class Running : State
        {
            public Running(CyclicalProcess it) : base(it) { }

            public override bool KeepWaiting => true;

            public override Action SubscribeToWhenRunning(Action action, Action value)
            {
                value?.Invoke();
                return action += value;
            }

            public override Action SubscribeToWhenCompleted(Action onCompleted, Action value) => onCompleted += value;

            public override void Start() { }

            public override void Stop()
            {
                SwitchState<Stopped>();
            }

            protected override void OnEnter()
            {
                It.WhenRunning?.Invoke();
            }
        }
    }
}
