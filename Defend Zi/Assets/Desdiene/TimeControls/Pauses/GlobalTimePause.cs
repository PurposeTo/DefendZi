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
    public class GlobalTimePause : MonoBehaviourExtContainer, ICyclicalProcess
    {
        private readonly TimePause _timePause;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mono">Игровой компонент, к жизненному циклу которого будет привязан данных объект.</param>
        /// <param name="name">Имя для логгирования.</param>
        public GlobalTimePause(MonoBehaviourExt mono, GlobalTimeScaler timeScaler, string name) : base(mono)
        {
            _timePause = new TimePause(timeScaler, name);
            mono.OnDestroyed += _timePause.Destroy;
        }

        public event Action WhenStarted
        {
            add => _timePause.WhenStarted += value;
            remove => _timePause.WhenStarted -= value;
        }

        public event Action WhenStopped
        {
            add => _timePause.WhenStopped += value;
            remove => _timePause.WhenStopped -= value;
        }

        public event Action<IProcessAccessor> OnChanged
        {
            add => _timePause.OnChanged += value;
            remove => _timePause.OnChanged -= value;
        }

        public string Name => _timePause.Name;

        public bool KeepWaiting => _timePause.KeepWaiting;

        public void Start() => _timePause.Start();

        public void Stop() => _timePause.Stop();
    }
}
