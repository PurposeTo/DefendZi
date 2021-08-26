using System;
using Desdiene.TimeControls.Pausables;
using Desdiene.Types.AtomicReferences;
using UnityEngine;

namespace Desdiene.TimeControls.Pausers
{
    public class TimePauser : ITimePauser
    {
        public string Name { get; }
        private readonly Ref<bool> isPauseRef;

        private readonly ITimePausable timePausable;

        public TimePauser(ITimePausable timePausable, string name) : this(timePausable, false, name) { }

        public TimePauser(ITimePausable timePausable, bool isPause, string name)
        {
            isPauseRef = new Ref<bool>(isPause);
            Name = name;
            this.timePausable = timePausable;
            Debug.Log($"Create timePauser with name: {Name}. TimePausable: {timePausable.GetType()}");
            this.timePausable.Add(this);
        }

        public event Action OnPauseChanged
        {
            add => isPauseRef.OnValueChanged += value;
            remove => isPauseRef.OnValueChanged -= value;
        }
        public bool IsPause => isPauseRef.Get();

        public void SetPause(bool isPause) => isPauseRef.Set(isPause);

        public void Destroy()
        {
            timePausable.Remove(this);
        }
    }
}