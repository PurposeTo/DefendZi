using System;

namespace Desdiene.Types.Processes
{
    public interface IProcessNotifier
    {
        event Action WhenRunning;

        event Action WhenCompleted;

        event Action<IProcessAccessor> OnChanged;
    }
}
