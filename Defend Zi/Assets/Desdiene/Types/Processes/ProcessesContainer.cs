using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Desdiene.Types.Processes
{
    public class ProcessesContainer : IProcesses
    {
        private readonly List<IMutableProcessGetter> _processes = new List<IMutableProcessGetter>();

        public ProcessesContainer(string name) : this(name, new List<IMutableProcessGetter>()) { }

        public ProcessesContainer(string name, List<IMutableProcessGetter> processes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            Name = name;
            processes.ForEach(process => Add(process));
        }

        public string Name { get; }
        public bool KeepWaiting { get; private set; }

        public event Action<IMutableProcessGetter> OnChanged;

        public void Add(IMutableProcessGetter process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (_processes.Contains(process))
            {
                Debug.LogError($"{Name} is already contains {process} with name {process.Name} in list!");
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

        public void Remove(IMutableProcessGetter process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (!_processes.Contains(process))
            {
                Debug.LogWarning($"{Name} is NOT contains {process} with name {process.Name} in list!");
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
            bool isChanged = KeepWaiting != keepWaiting;
            KeepWaiting = keepWaiting;
            LogAllProcesses();
            if (isChanged) OnChanged?.Invoke(this);
        }

        public void LogAllProcesses()
        {
            string logMessage = $"List in \"{Name}\" have {_processes.Count} items. KeepWaiting: {KeepWaiting}";
            _processes.ForEach(item => logMessage += $"\nName: {item.Name}. KeepWaiting: {item.KeepWaiting}");
            Debug.Log(logMessage);
        }

        private bool CalculateKeepWaiting()
        {
            return _processes.Any(process => process.KeepWaiting);
        }
    }
}
