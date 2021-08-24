namespace Desdiene.StateMachine.State
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

    public interface IStateEntryExitPoint<DynamicDataT>
    {
        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        /// <param name="enterParams">Объект с измененными состоянием данными. 
        /// Может быть null, если не было предшествующего состояния.</param>
        void OnEnter(DynamicDataT enterParams);

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        /// <returns>Объект с измененными состоянием данными.</returns>
        DynamicDataT OnExit();
    }
}
