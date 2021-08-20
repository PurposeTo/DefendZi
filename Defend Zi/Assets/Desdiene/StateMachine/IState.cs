using System;

namespace Desdiene.StateMachine
{
    public interface IState
    {
        /// <summary>
        /// Метод, выполняющийся по входу в состояние.
        /// </summary>
        Action OnEnter { get; }

        /// <summary>
        /// Метод, выполняющийся по выходу из состояния.
        /// </summary>
        Action OnExit { get; }
    }
}
