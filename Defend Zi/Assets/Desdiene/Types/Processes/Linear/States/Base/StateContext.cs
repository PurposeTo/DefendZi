using System;

namespace Desdiene.Types.Processes.Linear.States
{
    public class StateContext
    {
        public Action OnStarted { get; set; }
        public Action OnCompleted { get; set; }
        public Action OnChanged { get; set; }
    }
}
