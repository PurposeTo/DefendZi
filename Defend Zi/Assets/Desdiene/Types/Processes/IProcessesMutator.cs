namespace Desdiene.Types.Processes
{
    public interface IProcessesMutator
    {
        public void Add(IProcessAccessorNotifier[] processes);
        public void Add(IProcessAccessorNotifier process);
        public void Remove(IProcessAccessorNotifier process);
    }
}
