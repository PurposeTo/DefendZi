using System;

namespace Desdiene.Types.Processes.States.Base
{
    public class StateContext
    {
        public StateContext(Action onStarted, Action onComplited, Action onChanged)
        {
            OnStarted = onStarted;
            OnCompleted = onComplited;
            OnChanged = onChanged;
        }

        public Action OnStarted { get; }
        public Action OnCompleted { get; }
        public Action OnChanged { get; }
    }
}
