namespace Desdiene.Types.Processes
{
    public interface IProcessesSetter
    {
        public void Add(IMutableProcessGetter process);
        public void Remove(IMutableProcessGetter process);
    }
}
