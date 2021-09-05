namespace Desdiene.Types.Processes
{
    public interface IProcessesSetter
    {
        public void Add(IProcessGetterNotifier process);
        public void Remove(IProcessGetterNotifier process);
    }
}
