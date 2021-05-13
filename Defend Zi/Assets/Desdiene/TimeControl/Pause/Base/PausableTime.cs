using System;
using Desdiene.AtomicReference;

namespace Desdiene.TimeControl.Pause.Base
{
    public class PausableTime : IPausableTime
    {
        public string Name { get; }
        private readonly AtomicRef<bool> isPauseRef;

        public PausableTime(IPauser pauser, string name) : this(pauser, false, name) { }

        public PausableTime(IPauser pauser, bool isPause, string name)
        {
            isPauseRef = new AtomicRef<bool>(isPause);
            this.Name = name;
            pauser.Add(this);
        }

        public event Action OnPauseChanged
        {
            add { isPauseRef.OnValueChanged += value; }
            remove { isPauseRef.OnValueChanged -= value; }
        }
        public bool IsPause => isPauseRef.value;

        public void SetPause(bool isPause) => isPauseRef.Set(isPause);
    }
}