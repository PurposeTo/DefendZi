using System.Collections;
using Desdiene.Singleton.Unity;
using Desdiene.TimeControl.Pauser;
using Desdiene.TimeControl.Scale;
using UnityEngine;

namespace Desdiene.TimeControl.Pausable
{
    public class GlobalTimePausable : GlobalSingleton<GlobalTimePausable>, ITimePausable
    {
        //todo используется композиция, а не наследование потому, что GlobalPauser необходимо унаследовать от Singltone.
        private PausableTime pausable;

        protected override void AwakeSingleton()
        {
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pausable = new PausableTime(globalTimeScaler);
        }

        private IEnumerator Start()
        {
            var wait = new WaitForSecondsRealtime(10f);
            while (true)
            {
                LogAllPausers();
                yield return wait;
            }
        }

        public bool IsPause => pausable.IsPause;

        public void LogAllPausers()
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => pausable.LogAllPausers();
        }

        public void Add(ITimePauser pauser)
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => this.pausable.Add(pauser);
        }

        public void Remove(ITimePauser pauser)
        {
            // pauser инициализируется только после инициализации GlobalTimeScaler...
            GlobalTimeScaler.OnInited += (globalTimeScaler) => this.pausable.Remove(pauser);
        }
    }
}
