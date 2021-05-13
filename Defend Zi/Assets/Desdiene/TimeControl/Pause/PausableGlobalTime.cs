using System;
using Desdiene.Container;
using Desdiene.TimeControl.Pause.Base;
using UnityEngine;

namespace Desdiene.TimeControl.Pause
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    //Работать с UnityEngine.Time только внутри ЖЦ monoBehaviour.
    public class PausableGlobalTime : MonoBehaviourContainer, IPausableTime
    {
        private PausableTime pausableTime;

        public PausableGlobalTime(MonoBehaviour monoBehaviour, string name) : base(monoBehaviour)
        {
            GlobalPauser.InitializedInstance += (instance) =>
            {
                pausableTime = new PausableTime(instance, name);
            };
        }

        public string Name => pausableTime.Name;

        public bool IsPause => pausableTime.IsPause;

        public event Action OnPauseChanged
        {
            add { pausableTime.OnPauseChanged += value; }
            remove { pausableTime.OnPauseChanged -= value; }
        }

        public void SetPause(bool isPause) => pausableTime.SetPause(isPause);
    }
}
