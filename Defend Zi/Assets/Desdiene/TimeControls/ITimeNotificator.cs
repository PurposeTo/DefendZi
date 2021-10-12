using System;

namespace Desdiene.TimeControls
{
    public interface ITimeNotificator
    {
        event Action WhenStopped;
        event Action WhenRunning;
        event Action OnChanged;
    }
}
