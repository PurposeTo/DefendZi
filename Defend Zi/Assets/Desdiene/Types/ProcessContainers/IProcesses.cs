using Desdiene.Types.Processes;

namespace Desdiene.Types.ProcessContainers
{
    public interface IProcesses : IProcessAccessorNotifier, IProcessesMutator
    {
        /// <summary>
        /// Очистить список от всех процессов.
        /// Необходимо вызывать, если время жизни процесса больше, чем время жизни контейнера процессов, чтобы избежать утечек памяти.
        /// </summary>
        void Clear();
    }
}
