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

    public interface IStateEntryExitPoint<MutableDataT>
    {
        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        /// <param name="mutableData">Объект с измененными состоянием данными. 
        /// Может быть null, если не было предшествующего состояния.</param>
        void OnEnter(MutableDataT mutableData);

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        /// <returns>Объект с измененными состоянием данными.</returns>
        MutableDataT OnExit();
    }
}
