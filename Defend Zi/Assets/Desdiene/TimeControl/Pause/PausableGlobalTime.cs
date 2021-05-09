using System;
using Desdiene.AtomicReference;
using Desdiene.TimeControl.Pause;

namespace Assets.Desdiene.TimeControl.Pause
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    public class PausableGlobalTime
    {
        public string Name { get; }
        private readonly AtomicRef<bool> isPauseRef;

        public PausableGlobalTime(string name) : this(false, name) { }

        public PausableGlobalTime(bool isPause, string name)
        {
            isPauseRef = new AtomicRef<bool>(isPause);
            this.Name = name;

            GlobalPauser.Instance.Add(this);
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
