using System;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.TimeControls.Pauses
{
    /// <summary>
    /// Объект, позволяющий поставить время на паузу.
    /// При создании сам добавляется в нужный список.
    /// </summary>
    public class TimePause : IProcess
    {
        private readonly IProcess _itSelf;
        private readonly IProcesses _timePauses;

        public TimePause(IProcesses timePauses, string name)
        {
            _itSelf = new Process(name);
            _timePauses = timePauses;
            Debug.Log($"Create timePause with name: {Name}. Time: {timePauses.GetType().Name}");
            _timePauses.Add(this);
        }

        public event Action OnStarted
        {
            add => _itSelf.OnStarted += value;
            remove => _itSelf.OnStarted -= value;
        }

        public event Action OnCompleted
        {
            add => _itSelf.OnCompleted += value;
            remove => _itSelf.OnCompleted -= value;
        }

        public event Action<IProcessGetter> OnChanged
        {
            add => _itSelf.OnChanged += value;
            remove => _itSelf.OnChanged -= value;
        }

        public string Name => _itSelf.Name;

        public bool KeepWaiting => _itSelf.KeepWaiting;

        public void Destroy()
        {
            _timePauses.Remove(this);
        }

        public IProcess Start() => _itSelf.Start();

        public IProcess Complete() => _itSelf.Complete();
    }
}