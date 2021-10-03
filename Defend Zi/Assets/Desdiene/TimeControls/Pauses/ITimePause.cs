using Desdiene.Types.Processes;

namespace Desdiene.TimeControls.Pauses
{
    public interface ITimePause : ICyclicalProcess
    {
        void Destroy();
    }
}
