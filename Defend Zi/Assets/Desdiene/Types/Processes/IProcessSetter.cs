namespace Desdiene.Types.Processes
{
    public interface IProcessSetter
    {
        IProcess Start();
        IProcess Complete();
    }
}
