namespace Desdiene.Types.Processes
{
    public interface IProcessesMutator
    {
        public void Add(IProcessGetterNotifier[] processes);
        public void Add(IProcessGetterNotifier process);
        public void Remove(IProcessGetterNotifier process);
    }
}
