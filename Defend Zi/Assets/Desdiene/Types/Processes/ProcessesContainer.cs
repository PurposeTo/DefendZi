using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Desdiene.Types.Processes
{
    public class ProcessesContainer : IProcesses
    {
        private readonly List<IProcessGetterNotifier> _processes = new List<IProcessGetterNotifier>();
        private readonly IProcess _process;

        public ProcessesContainer(string name) : this(name, new List<IProcessGetterNotifier>()) { }

        public ProcessesContainer(string name, List<IProcessGetterNotifier> processes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _process = new Process(name);
            processes.ForEach(process => Add(process));
        }

        event Action IProcessNotifier.OnStarted
        {
            add => _process.OnStarted += value;
            remove => _process.OnStarted -= value;
        }

        event Action IProcessNotifier.OnCompleted
        {
            add => _process.OnCompleted += value;
            remove => _process.OnCompleted -= value;
        }

        event Action<IProcessGetter> IProcessNotifier.OnChanged
        {
            add => _process.OnChanged += value;
            remove => _process.OnChanged -= value;
        }

        string IProcessGetter.Name => Name;
        bool IProcessGetter.KeepWaiting => KeepWaiting;

        private string Name => _process.Name;
        private bool KeepWaiting => _process.KeepWaiting;

        void IProcessesSetter.Add(IProcessGetterNotifier[] processes)
        {
            if (processes is null)
            {
                throw new ArgumentNullException(nameof(processes));
            }

            Array.ForEach(processes, process => Add(process));
        }

        void IProcessesSetter.Add(IProcessGetterNotifier process) => Add(process);

        void IProcessesSetter.Remove(IProcessGetterNotifier process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (!_processes.Contains(process))
            {
                Debug.LogWarning($"{Name} is NOT contains {process} with name {process.Name} in list!");
            }
            else
            {
                _processes.Remove(process);
                process.OnChanged -= SetActualState;
                SetActualState(process);
            }
        }

        private void Add(IProcessGetterNotifier process)
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
                process.OnChanged += SetActualState;
                SetActualState(process);
            }
        }

        private void SetActualState(IProcessGetter _)
        {
            if (_processes.Any(process => process.KeepWaiting)) Start();
            else Complete();
            LogAllProcesses();
        }

        private void Start() => _process.Start();

        private void Complete() => _process.Complete();

        private void LogAllProcesses()
        {
            string logMessage = $"List in \"{Name}\" have {_processes.Count} items. KeepWaiting: {KeepWaiting}";
            _processes.ForEach(item => logMessage += $"\nName: {item.Name}. KeepWaiting: {item.KeepWaiting}");
            Debug.Log(logMessage);
        }
    }
}
