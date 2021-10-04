using System;

namespace Desdiene.Types.Processes
{
    public interface ICyclicalProcessNotifier : IProcessNotifier
    {
        /// <summary>
        /// Событие о начале процесса. Если процесс уже был начат, то вызовется сразу.
        /// </summary>
        event Action WhenStarted;

        /// <summary>
        /// Событие об остановке процесса. Если процесс уже был выполнен, то вызовется сразу.
        /// </summary>
        event Action WhenStopped;
    }
}
