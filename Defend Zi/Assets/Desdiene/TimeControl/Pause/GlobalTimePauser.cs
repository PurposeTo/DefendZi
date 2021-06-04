using System.Collections;
using Desdiene.Singleton.Unity;
using Desdiene.TimeControl.Pause.Base;
using Desdiene.TimeControl.Scale;
using UnityEngine;

namespace Desdiene.TimeControl.Pause
{
    public class GlobalTimePauser : GlobalSingleton<GlobalTimePauser>, IPauser
    {
        //todo используется композиция, а не наследование потому, что GlobalPauser необходимо унаследовать от Singltone.
        private Pauser pauser;

        protected override void AwakeSingleton()
        {
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pauser = new Pauser(globalTimeScaler);
        }

        private IEnumerator Start()
        {
            var wait = new WaitForSecondsRealtime(10f);
            while (true)
            {
                LogAllPausables();
                yield return wait;
            }
        }

        public bool IsPause => pauser.IsPause;

        public void LogAllPausables()
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pauser.LogAllPausables();
        }

        public void Add(IPausableTime pausable)
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pauser.Add(pausable);
        }

        public void Remove(IPausableTime pausable)
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pauser.Remove(pausable);
        }
    }
}
