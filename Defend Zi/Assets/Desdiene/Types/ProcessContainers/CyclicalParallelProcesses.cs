using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.Types.ProcessContainers
{
    /// <summary>
    /// Представляет контейнер для цикличных и линейных процессов.
    /// </summary>
    public class CyclicalParallelProcesses : ICyclicalProcesses
    {
        private readonly List<IProcessAccessorNotifier> _processes = new List<IProcessAccessorNotifier>();
        private readonly ICyclicalProcess _process;

        public CyclicalParallelProcesses(string name) : this(name, new List<IProcessAccessorNotifier>()) { }

        public CyclicalParallelProcesses(string name, List<IProcessAccessorNotifier> processes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _process = new CyclicalProcess(name);
            processes.ForEach(process => Add(process));
        }

        event Action ICyclicalProcessNotifier.OnStarted
        {
            add => _process.OnStarted += value;
            remove => _process.OnStarted -= value;
        }

        event Action ICyclicalProcessNotifier.OnStopped
        {
            add => _process.OnStopped += value;
            remove => _process.OnStopped -= value;
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

        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier[] processes)
        {
            if (processes is null)
            {
                throw new ArgumentNullException(nameof(processes));
            }

            Array.ForEach(processes, process => Add(process));
        }

        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier process) => Add(process);

        void ICyclicalProcessesMutator.Remove(IProcessAccessorNotifier process) => Remove(process);

        void ICyclicalProcesses.Clear()
        {
            List<IProcessAccessorNotifier> processes = new List<IProcessAccessorNotifier>(_processes);
            processes.ForEach(process => Remove(process));
            Debug.Assert(_processes.Count == 0);
        }

        private void Add(IProcessAccessorNotifier process)
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

        private void Remove(IProcessAccessorNotifier process)
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

        private void LogAllProcesses()
        {
            string logMessage = $"List in \"{Name}\" have {_processes.Count} items. KeepWaiting: {KeepWaiting}";
            _processes.ForEach(item => logMessage += $"\nName: {item.Name}. KeepWaiting: {item.KeepWaiting}");
            Debug.Log(logMessage);
        }
    }
}
