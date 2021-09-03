namespace Desdiene.Types.Processes
{
    public interface IProcessGetter
    {
        string Name { get; }
        bool KeepWaiting { get; }
    }
}
