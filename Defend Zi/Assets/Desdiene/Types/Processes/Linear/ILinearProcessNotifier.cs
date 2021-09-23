using System;

namespace Desdiene.Types.Processes
{
    public interface ILinearProcessNotifier : IProcessNotifier
    {
        /// <summary>
        /// Событие о начале процесса. Если процесс уже был начат или выполнен, то вызовется сразу.
        /// </summary>
        event Action OnStarted;

        /// <summary>
        /// Событие об выполнении процесса. Если процесс уже был выполнен, то вызовется сразу.
        /// </summary>
        event Action OnCompleted;
    }
}
