using System;
using Desdiene.AtomicReference;

namespace Desdiene.TimeControl.Pause.Base
{
    public class PausableTime : IPausableTime
    {
        public string Name { get; }
        private readonly AtomicRef<bool> isPauseRef;

        private readonly IPauser pauser;

        public PausableTime(IPauser pauser, string name) : this(pauser, false, name) { }

        public PausableTime(IPauser pauser, bool isPause, string name)
        {
            isPauseRef = new AtomicRef<bool>(isPause);
            this.Name = name;
            this.pauser = pauser;
            pauser.Add(this);
        }

        public event Action OnPauseChanged
        {
            add { isPauseRef.OnValueChanged += value; }
            remove { isPauseRef.OnValueChanged -= value; }
        }
        public bool IsPause => isPauseRef.value;

        public void SetPause(bool isPause) => isPauseRef.Set(isPause);

        public void Destroy()
        {
            pauser.Remove(this);
        }
    }
}