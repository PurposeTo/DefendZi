using Desdiene.Singleton;
using Desdiene.TimeControl.Pause.Base;
using Desdiene.TimeControl.Scale;

namespace Desdiene.TimeControl.Pause
{
    public class GlobalPauser : LazySingleton<GlobalPauser>, IPauser
    {
        private readonly Pauser pauser;

        public GlobalPauser()
        {
            pauser = new Pauser(new GlobalTimeScaler());
        }

        public bool IsPause => pauser.IsPause;

        public void Add(PausableTime pausable) => pauser.Add(pausable);

        public void Remove(PausableTime pausable) => pauser.Remove(pausable);
    }
}
