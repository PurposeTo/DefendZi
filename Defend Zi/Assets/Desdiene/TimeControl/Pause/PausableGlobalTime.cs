using System;
using Desdiene.Container;
using Desdiene.SuperMonoBehaviourAsset;
using Desdiene.TimeControl.Pause.Base;
using UnityEngine;

namespace Desdiene.TimeControl.Pause
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    //Работать с UnityEngine.Time только внутри ЖЦ monoBehaviour.
    public class PausableGlobalTime : SuperMonoBehaviourContainer, IPausableTime
    {
        private PausableTime pausableTime;

        public PausableGlobalTime(SuperMonoBehaviour superMono, string name) : base(superMono)
        {
            GlobalPauser.OnInited += (instance) =>
            {
                pausableTime = new PausableTime(instance, name);
                superMono.OnDestroyed += pausableTime.Destroy;
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
