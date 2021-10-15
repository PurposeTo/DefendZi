using System;

namespace Desdiene.Types.Processes
{
    public partial class OptionalLinearProcess
    {
        private class CreatedNonWaiting : State
        {
            public CreatedNonWaiting(OptionalLinearProcess it) : base(it) { }

            public override bool KeepWaiting => false;

            public override void Start()
            {
                SwitchState<Running>();
            }

            public override void Complete()
            {
                SwitchState<Running>().Complete();
            }

            public override Action SubscribeToWhenRunning(Action action, Action value) => action += value;
            public override Action SubscribeToWhenCompleted(Action onCompleted, Action value) => onCompleted += value;
        }
    }
}
