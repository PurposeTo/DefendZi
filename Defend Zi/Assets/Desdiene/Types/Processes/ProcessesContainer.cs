using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Desdiene.Types.Processes
{
    public class ProcessesContainer : IProcesses
    {
        private readonly List<IProcess> _processes;

        public ProcessesContainer(string name) : this(name, new List<IProcess>()) { }

        public ProcessesContainer(string name, List<IProcess> processes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            Name = name;
            _processes = processes;
            Update(this);
        }

        public string Name { get; }
        public bool KeepWaiting { get; private set; }

        public event Action<IMutableProcessGetter> OnChanged;

        public void Add(IProcess process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (_processes.Contains(process))
            {
                Debug.LogError($"{this.GetType().Name} is already contains {process} with name {process.Name} in list!");
            }
            else if (_processes.Any(item => item.Name == process.Name))
            {
                Debug.LogError($"Processes list is already contains item with name {process.Name}, " +
                    "but it is not the same item!");
            }
            else
            {
                _processes.Add(process);
                Update(process);
                process.OnChanged += Update;
            }
        }

        public void Remove(IProcess process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (!_processes.Contains(process))
            {
                Debug.LogWarning($"{this.GetType().Name} is NOT contains {process} with name {process.Name} in list!");
            }
            else
            {
                _processes.Remove(process);
                Update(process);
                process.OnChanged -= Update;
            }
        }

        private void Update(IMutableProcessGetter process)
        {
            bool keepWaiting = CalculateKeepWaiting();
            if (KeepWaiting != keepWaiting)
            {
                KeepWaiting = keepWaiting;
                LogAllProcesses();
                OnChanged?.Invoke(this);
            }
        }

        public void LogAllProcesses()
        {
            string logMessage = $"{nameof(ProcessesContainer)} list have {_processes.Count} items. KeepWaiting: {KeepWaiting}";
            _processes.ForEach(item => logMessage += $"\nName: {item.Name}. KeepWaiting: {item.KeepWaiting}");
            Debug.Log(logMessage);
        }

        private bool CalculateKeepWaiting()
        {
            return _processes.Any(process => process.KeepWaiting);
        }
    }
}
