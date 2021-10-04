using System;

namespace Desdiene.TimeControls
{
    public interface ITimeNotification
    {
        event Action WhenStopped;
        event Action WhenRunning;
        event Action OnChanged;
    }
}
