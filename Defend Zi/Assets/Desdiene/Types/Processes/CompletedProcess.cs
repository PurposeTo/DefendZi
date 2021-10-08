using System;

namespace Desdiene.Types.Processes
{
    public class CompletedProcess : IProcessAccessorNotifier
    {
        string IProcessAccessor.Name => "Законченный процесс";

        bool IProcessAccessor.KeepWaiting => false;

        event Action IProcessNotifier.WhenRunning
        {
            add => value?.Invoke();
            remove { }
        }

        event Action IProcessNotifier.WhenCompleted
        {
            add => value?.Invoke();
            remove { }
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add { }
            remove { }
        }
    }
}
