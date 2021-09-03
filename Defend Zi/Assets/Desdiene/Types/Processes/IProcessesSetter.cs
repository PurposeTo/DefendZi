namespace Desdiene.Types.Processes
{
    public interface IProcessesSetter
    {
        public void Add(IProcess process);
        public void Remove(IProcess process);
    }
}
