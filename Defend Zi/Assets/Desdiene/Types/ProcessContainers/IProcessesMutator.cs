using Desdiene.Types.Processes;

namespace Desdiene.Types.ProcessContainers
{
    public interface IProcessesMutator
    {
        void Add(IProcessAccessorNotifier[] processes);
        void Add(IProcessAccessorNotifier process);
        void Remove(IProcessAccessorNotifier process);
    }
}
