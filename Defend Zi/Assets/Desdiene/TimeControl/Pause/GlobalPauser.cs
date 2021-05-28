using Desdiene.Singleton;
using Desdiene.TimeControl.Pause.Base;
using Desdiene.TimeControl.Scale;

namespace Desdiene.TimeControl.Pause
{
    public class GlobalPauser : SingletonMonoBehaviourExt<GlobalPauser>, IPauser
    {
        //todo используется композиция, а не наследование потому, что GlobalPauser необходимо унаследовать от Singltone.
        private Pauser pauser;

        protected override void AwakeSingleton()
        {
            pauser = new Pauser(new GlobalTimeScaler(this));
        }

        public bool IsPause => pauser.IsPause;

        public void LogAllPausables() => pauser.LogAllPausables();

        public void Add(IPausableTime pausable) => pauser.Add(pausable);

        public void Remove(IPausableTime pausable) => pauser.Remove(pausable);
    }
}
