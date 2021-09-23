using System;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.TimeControls.Pauses
{
    /// <summary>
    /// Объект, позволяющий поставить время на паузу.
    /// При создании сам добавляется в нужный список.
    /// </summary>
    public class TimePause : ICyclicalProcess
    {
        private readonly ICyclicalProcess _itSelf;
        private readonly ICyclicalProcessesMutator _timePauses;

        public TimePause(ICyclicalProcessesMutator timePauses, string name)
        {
            _itSelf = new CyclicalProcess(name);
            _timePauses = timePauses;
            Debug.Log($"Create timePause with name: {Name}. Time: {timePauses.GetType().Name}");
            _timePauses.Add(this);
        }

        public event Action OnStarted
        {
            add => _itSelf.OnStarted += value;
            remove => _itSelf.OnStarted -= value;
        }

        public event Action OnStopped
        {
            add => _itSelf.OnStopped += value;
            remove => _itSelf.OnStopped -= value;
        }

        public event Action<IProcessAccessor> OnChanged
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

        public void Start() => _itSelf.Start();

        public void Stop() => _itSelf.Stop();
    }
}