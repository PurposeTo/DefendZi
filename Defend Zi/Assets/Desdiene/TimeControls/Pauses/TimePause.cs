using System;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.TimeControls.Pauses
{
    public class TimePause : IProcess
    {
        private readonly IProcess _process;
        private readonly IProcesses _timePauses;

        public TimePause(IProcesses timePauses, string name) : this(timePauses, name, false) { }

        public TimePause(IProcesses timePauses, string name, bool isPause)
        {
            _process = new Process(name, isPause);
            _timePauses = timePauses;
            Debug.Log($"Create timePause with name: {Name}. Time: {timePauses.GetType()}");
            _timePauses.Add(this);
        }

        public string Name => _process.Name;

        public bool KeepWaiting => _process.KeepWaiting;

        public event Action<IMutableProcessGetter> OnChanged
        {
            add => _process.OnChanged += value;
            remove => _process.OnChanged -= value;
        }

        public void Destroy()
        {
            _timePauses.Remove(this);
        }

        public void Set(bool keepWaiting) => _process.Set(keepWaiting);
    }
}