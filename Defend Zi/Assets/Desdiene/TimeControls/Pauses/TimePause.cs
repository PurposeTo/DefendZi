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
    public sealed class TimePause : ITimePause
    {
        private readonly ICyclicalProcess _itSelf;
        private readonly ICyclicalProcessesMutator _timePauses;

        public TimePause(ICyclicalProcessesMutator timePauses, string name)
        {
            _itSelf = new CyclicalProcess(name);
            _timePauses = timePauses ?? throw new ArgumentNullException(nameof(timePauses));
            Debug.Log($"Create timePause with name: {_itSelf.Name}. Time: {timePauses.GetType().Name}");
            _timePauses.Add(this);
        }

        event Action ICyclicalProcessNotifier.WhenStarted
        {
            add => _itSelf.WhenStarted += value;
            remove => _itSelf.WhenStarted -= value;
        }

        event Action ICyclicalProcessNotifier.WhenStopped
        {
            add => _itSelf.WhenStopped += value;
            remove => _itSelf.WhenStopped -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _itSelf.OnChanged += value;
            remove => _itSelf.OnChanged -= value;
        }

        string IProcessAccessor.Name => _itSelf.Name;

        bool IProcessAccessor.KeepWaiting => _itSelf.KeepWaiting;

        void ICyclicalProcessMutator.Start() => _itSelf.Start();

        void ICyclicalProcessMutator.Stop() => _itSelf.Stop();

        void ITimePause.Destroy()
        {
            _timePauses.Remove(this);
        }
    }
}