using System;

namespace Desdiene.Types.Processes.Cyclical.States
{
    public class StateContext
    {
        public Action OnStarted { get; set; }
        public Action OnStopped { get; set; }
        public Action OnChanged { get; set; }
    }
}
