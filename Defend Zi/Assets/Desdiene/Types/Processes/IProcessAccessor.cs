namespace Desdiene.Types.Processes
{
    public interface IProcessAccessor
    {
        /// <summary>
        /// Название процесса
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Выполняется ли в данный момент процесс
        /// </summary>
        bool KeepWaiting { get; }
    }
}
