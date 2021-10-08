namespace Desdiene.StateMachines.States
{
    public interface IStateEntryExitPoint
    {
        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        void OnExit();
    }

    public interface IStateEntryExitPoint<StateContextT>
    {
        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        void OnEnter(StateContextT stateContext);

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        void OnExit(StateContextT stateContext);
    }
}
