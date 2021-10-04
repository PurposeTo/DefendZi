using System;

namespace Desdiene.Types.Processes
{
    public interface IProcessNotifier
    {
        /// <summary>
        /// Событие о изменении состоянии процесса.
        /// </summary>
        event Action<IProcessAccessor> OnChanged;
    }
}
