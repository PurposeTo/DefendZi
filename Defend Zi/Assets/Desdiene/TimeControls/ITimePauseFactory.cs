using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Processes;

namespace Desdiene.TimeControls
{
    public interface ITimePauseFactory
    {
        ICyclicalProcess CreatePause(MonoBehaviourExt mono, string name);
    }
}
