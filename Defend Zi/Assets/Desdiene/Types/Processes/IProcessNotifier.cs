using System;

namespace Desdiene.Types.Processes
{
    public interface IProcessNotifier
    {
        event Action WhenStarted;

        event Action WhenCompleted;

        event Action<IProcessAccessor> OnChanged;
    }
}
