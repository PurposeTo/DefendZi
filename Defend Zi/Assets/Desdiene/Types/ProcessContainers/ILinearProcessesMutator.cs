using Desdiene.Types.Processes;

namespace Desdiene.Types.ProcessContainers
{
    public interface ILinearProcessesMutator
    {
        void Add(ILinearProcessAccessorNotifier[] processes);
        void Add(ILinearProcessAccessorNotifier process);
        void Remove(ILinearProcessAccessorNotifier process);
    }
}
