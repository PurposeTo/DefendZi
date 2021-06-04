using System;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtention;
using Desdiene.TimeControl.Pause.Base;

namespace Desdiene.TimeControl.Pause
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    //Работать с UnityEngine.Time только внутри ЖЦ monoBehaviour.
    public class PausableGlobalTime : MonoBehaviourExtContainer, IPausableTime
    {
        private readonly PausableTime pausableTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superMono">Игровой компонент, к жизненному циклу которого будет привязан данных объект.</param>
        /// <param name="globalTimePauser">Ссылка на GlobalPauser</param>
        /// <param name="name">Имя для логгирования.</param>
        public PausableGlobalTime(MonoBehaviourExt superMono, GlobalTimePauser globalTimePauser, string name) : base(superMono)
        {
            pausableTime = new PausableTime(globalTimePauser, name);
            superMono.OnDestroyed += pausableTime.Destroy;
        }

        public string Name => pausableTime.Name;

        public bool IsPause => pausableTime.IsPause;

        public event Action OnPauseChanged
        {
            add => pausableTime.OnPauseChanged += value;
            remove => pausableTime.OnPauseChanged -= value;
        }

        public void SetPause(bool isPause) => pausableTime.SetPause(isPause);
    }
}
