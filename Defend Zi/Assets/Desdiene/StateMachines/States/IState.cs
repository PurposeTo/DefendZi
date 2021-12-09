namespace Desdiene.StateMachines.States
{
    public interface IState
    {
        string Name { get; }

        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        void OnExit();
    }
}
