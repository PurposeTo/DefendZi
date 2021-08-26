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
        /// <returns>Новое состояние.</returns>
        AbstractStateT Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;

        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <param name="predicate">Условие выбора нового состояния.</param>
        /// <returns>Новое состояние.</returns>
        AbstractStateT Switch(Predicate<AbstractStateT> predicate);

        /// <summary>
        /// Существует ли хотя бы одно состояние, удовлетворяющее условию?
        /// </summary>
        bool Any(Predicate<AbstractStateT> predicate);
    }

    public interface IStateSwitcher<AbstractStateT, MutableDataT>
        where AbstractStateT : IStateEntryExitPoint<MutableDataT>
        where MutableDataT : class
    {
        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип нового состояния.</typeparam>
        /// <returns>Новое состояние.</returns>
        AbstractStateT Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;

        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <param name="predicate">Условие выбора нового состояния.</param>
        /// <returns>Новое состояние.</returns>
        AbstractStateT Switch(Predicate<AbstractStateT> predicate);

        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        /// <param name="newState">Новое состояние.</param>
        /// <returns>Новое состояние.</returns>
        AbstractStateT Switch(AbstractStateT newState);

        /// <summary>
        /// Существует ли хотя бы одно состояние, удовлетворяющее условию?
        /// </summary>
        bool Any(Predicate<AbstractStateT> predicate);
    }
}
