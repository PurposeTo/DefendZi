using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Scalers;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls.Pauses
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    //Работать с UnityEngine.Time только внутри ЖЦ monoBehaviour.
    public class GlobalTimePause : MonoBehaviourExtContainer, ITimePause
    {
        private readonly ITimePause _timePause;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mono">Игровой компонент, к жизненному циклу которого будет привязан данных объект.</param>
        /// <param name="name">Имя для логгирования.</param>
        public GlobalTimePause(MonoBehaviourExt mono, GlobalTime globalTime, string name) : base(mono)
        {
            _timePause = new TimePause(globalTime, name);
            mono.OnDestroyed += _timePause.Destroy;
        }

        event Action ICyclicalProcessNotifier.WhenStarted
        {
            add => _timePause.WhenStarted += value;
            remove => _timePause.WhenStarted -= value;
        }

        event Action ICyclicalProcessNotifier.WhenStopped
        {
            add => _timePause.WhenStopped += value;
            remove => _timePause.WhenStopped -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _timePause.OnChanged += value;
            remove => _timePause.OnChanged -= value;
        }

        string IProcessAccessor.Name => _timePause.Name;

        bool IProcessAccessor.KeepWaiting => _timePause.KeepWaiting;

        void ICyclicalProcessMutator.Start() => _timePause.Start();

        void ICyclicalProcessMutator.Stop() => _timePause.Stop();

        void ITimePause.Destroy() => _timePause.Destroy();
    }
}
