using System;
using Desdiene.StateMachine.State;

namespace Desdiene.StateMachine.StateSwitching
{
    public interface IStateSwitcher<AbstractStateT> where AbstractStateT : IStateEntryExitPoint
    {
        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип нового состояния.</typeparam>
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;

        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <param name="predicate">Условие выбора нового состояния.</param>
        void Switch(Predicate<AbstractStateT> predicate);
    }

    public interface IStateSwitcher<AbstractStateT, DynamicDataT>
        where AbstractStateT : IStateEntryExitPoint<DynamicDataT>
        where DynamicDataT : class
    {
        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип нового состояния.</typeparam>
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;

        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <param name="predicate">Условие выбора нового состояния.</param>
        void Switch(Predicate<AbstractStateT> predicate);
    }
}
