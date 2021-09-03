using System;

namespace Desdiene.Types.Processes
{
    public class Process : IProcess
    {
        public Process(string name) : this(name, false) { }

        public Process(string name, bool keepWaiting)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            Name = name;
            Set(keepWaiting);
        }

        public event Action<IMutableProcessGetter> OnChanged;

        public string Name { get; }
        public bool KeepWaiting { get; private set; }

        public void Set(bool keepWaiting)
        {
            if (KeepWaiting != keepWaiting)
            {
                KeepWaiting = keepWaiting;
                OnChanged?.Invoke(this);
            }
        }
    }
}
