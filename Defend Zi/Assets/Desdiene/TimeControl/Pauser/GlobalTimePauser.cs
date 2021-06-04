using System;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtention;
using Desdiene.TimeControl.Pausable;

namespace Desdiene.TimeControl.Pauser
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    //Работать с UnityEngine.Time только внутри ЖЦ monoBehaviour.
    public class GlobalTimePauser : MonoBehaviourExtContainer, ITimePauser
    {
        private readonly TimePauser timePauser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superMono">Игровой компонент, к жизненному циклу которого будет привязан данных объект.</param>
        /// <param name="globalTimePausable">Ссылка на GlobalPauser</param>
        /// <param name="name">Имя для логгирования.</param>
        public GlobalTimePauser(MonoBehaviourExt superMono, GlobalTimePausable globalTimePausable, string name) : base(superMono)
        {
            timePauser = new TimePauser(globalTimePausable, name);
            superMono.OnDestroyed += timePauser.Destroy;
        }

        public string Name => timePauser.Name;

        public bool IsPause => timePauser.IsPause;

        public event Action OnPauseChanged
        {
            add => timePauser.OnPauseChanged += value;
            remove => timePauser.OnPauseChanged -= value;
        }

        public void SetPause(bool isPause) => timePauser.SetPause(isPause);
    }
}
