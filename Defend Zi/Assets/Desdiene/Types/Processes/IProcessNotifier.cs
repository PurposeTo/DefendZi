using System;

namespace Desdiene.Types.Processes
{
    public interface IProcessNotifier
    {
        /// <summary>
        /// Событие о начале процесса. Если процесс уже был начат, то вызовется сразу.
        /// </summary>
        event Action OnStarted;

        /// <summary>
        /// Событие об выполнении процесса. Если процесс уже был выполнен, то вызовется сразу.
        /// </summary>
        event Action OnCompleted;

        /// <summary>
        /// Событие о изменении состоянии процесса.
        /// </summary>
        event Action<IProcessGetter> OnChanged;
    }
}
