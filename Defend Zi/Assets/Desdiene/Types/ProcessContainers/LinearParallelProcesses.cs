using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.Types.ProcessContainers
{
    /// <summary>
    /// Представляет контейнер только для линейных процессов.
    /// </summary>
    public class LinearParallelProcesses : ILinearProcesses
    {
        private readonly List<ILinearProcessAccessorNotifier> _processes = new List<ILinearProcessAccessorNotifier>();
        private readonly ILinearProcess _process;

        public LinearParallelProcesses(string name) : this(name, new List<ILinearProcessAccessorNotifier>()) { }

        public LinearParallelProcesses(string name, List<ILinearProcessAccessorNotifier> processes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _process = new OptionalLinearProcess(name);
            processes.ForEach(process => Add(process));
        }

        event Action ILinearProcessNotifier.OnStarted
        {
            add => _process.OnStarted += value;
            remove => _process.OnStarted -= value;
        }

        event Action ILinearProcessNotifier.OnCompleted
        {
            add => _process.OnCompleted += value;
            remove => _process.OnCompleted -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _process.OnChanged += value;
            remove => _process.OnChanged -= value;
        }

        string IProcessAccessor.Name => Name;
        bool IProcessAccessor.KeepWaiting => KeepWaiting;

        private string Name => _process.Name;
        private bool KeepWaiting => _process.KeepWaiting;

        void ILinearProcessesMutator.Add(ILinearProcessAccessorNotifier[] processes)
        {
            if (processes is null)
            {
                throw new ArgumentNullException(nameof(processes));
            }

            Array.ForEach(processes, process => Add(process));
        }

        void ILinearProcessesMutator.Add(ILinearProcessAccessorNotifier process) => Add(process);

        void ILinearProcessesMutator.Remove(ILinearProcessAccessorNotifier process) => Remove(process);

        void ILinearProcesses.Clear()
        {
            List<ILinearProcessAccessorNotifier> processes = new List<ILinearProcessAccessorNotifier>(_processes);
            processes.ForEach(process => Remove(process));
            Debug.Assert(_processes.Count == 0);
        }

        private void Add(ILinearProcessAccessorNotifier process)
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

        private void Remove(ILinearProcessAccessorNotifier process)
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

        private void SetActualState(IProcessAccessor _)
        {
            if (_processes.Any(process => process.KeepWaiting)) Start();
            else Stop();
            LogAllProcesses();
        }

        private void Start() => _process.Start();

        private void Stop() => _process.Stop();

        public void LogAllProcesses()
        {
            string logMessage = $"List in \"{Name}\" have {_processes.Count} items. KeepWaiting: {KeepWaiting}";
            _processes.ForEach(item => logMessage += $"\nName: {item.Name}. KeepWaiting: {item.KeepWaiting}");
            Debug.Log(logMessage);
        }
    }
}
