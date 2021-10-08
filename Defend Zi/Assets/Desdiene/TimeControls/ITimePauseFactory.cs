using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls
{
    public interface ITimePauseFactory
    {
        IProcess CreatePause(MonoBehaviourExt mono, string name);
    }
}
