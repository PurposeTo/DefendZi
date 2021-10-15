using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.StateMachines.States;

namespace Desdiene.StateMachines.StateSwitchers
{
    public class StateSwitcher<AbstractStateT> :
        IStateSwitcher<AbstractStateT>
        where AbstractStateT : class, IStateEntryExitPoint
    {
        private readonly List<AbstractStateT> _allStates;
        private AbstractStateT _currentState;

        public StateSwitcher(AbstractStateT currentState, List<AbstractStateT> allStates)
        {
            _currentState = currentState ?? throw new ArgumentNullException(nameof(currentState));
            _allStates = allStates ?? throw new ArgumentNullException(nameof(allStates));
            _currentState.OnEnter();
        }

        AbstractStateT IStateSwitcher<AbstractStateT>.CurrentState => _currentState;

        /// <summary>
        /// Сменить состояние на указанное по типу.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип искомого состояния.</typeparam>
        /// <returns>Новое состояние.</returns>
        AbstractStateT IStateSwitcher<AbstractStateT>.Switch<ConcreteStateT>()
        {
            AbstractStateT newState = _allStates.Single(it => it is ConcreteStateT);
            return Switch(newState);
        }

        /// <summary>
        /// Сменить состояние на подходящие по условию.
        /// </summary>
        ///<exception cref="InvalidOperationException">Если найдено 0 или >1 элемента по указанным условиям</exception>
        /// <param name="predicate">Условие поиска состояния.</param>
        /// <returns>Новое состояние.</returns>
        AbstractStateT IStateSwitcher<AbstractStateT>.Switch(Predicate<AbstractStateT> predicate)
        {
            AbstractStateT newState = _allStates.Single(state => predicate.Invoke(state));
            return Switch(newState);
        }

        bool IStateSwitcher<AbstractStateT>.Any(Predicate<AbstractStateT> predicate) => _allStates.Exists(predicate);

        AbstractStateT IStateSwitcher<AbstractStateT>.Switch(AbstractStateT newState) => Switch(newState);

        private AbstractStateT Switch(AbstractStateT newState)
        {
            if (!_allStates.Contains(newState))
            {
                throw new InvalidOperationException("You need to add the state to all states, before switching");
            }

            if (newState == _currentState) return _currentState;

            _currentState.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
            return _currentState;
        }
    }
}
