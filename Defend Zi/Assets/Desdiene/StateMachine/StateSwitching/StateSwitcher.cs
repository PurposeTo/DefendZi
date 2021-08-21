using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.StateMachine.State;
using Desdiene.Types.AtomicReference;

namespace Desdiene.StateMachine.StateSwitching
{
    public class StateSwitcher<AbstractStateT> : IStateSwitcher<AbstractStateT> where AbstractStateT : IStateEntryExitPoint
    {
        private readonly List<AbstractStateT> _allStates;
        private readonly IRef<AbstractStateT> _refCurrentState;

        public StateSwitcher(IRef<AbstractStateT> refCurrentState) : this(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
        {
            _allStates = allStates ?? throw new ArgumentNullException(nameof(allStates));
            _refCurrentState = refCurrentState ?? throw new ArgumentNullException(nameof(refCurrentState));
        }

        /// <summary>
        /// Был ли первоначальное включение состояния?
        /// Не реализован паттерн состояния, т.к. контексты бы пересекались и код был бы нечитабельным.
        /// </summary>
        private bool IsStarted = false;
        private AbstractStateT CurrentState
        {
            get
            {
                return _refCurrentState.Get() ?? throw new ArgumentNullException(nameof(CurrentState));
            }
            set
            {
                _refCurrentState.Set(value);
            }
        }

        /// <summary>
        /// Сменить состояние на указанное по типу.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип искомого состояния.</typeparam>
        public void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT
        {
            AbstractStateT state = _allStates.Single(it => it is ConcreteStateT);
            Switch(state);
        }

        /// <summary>
        /// Сменить состояние на подходящие по условию.
        /// </summary>
        ///<exception cref="InvalidOperationException">Если найдено 0 или >1 элемента по указанным условиям</exception>
        /// <param name="predicate">Условие поиска состояния.</param>
        public void Switch(Predicate<AbstractStateT> predicate)
        {
            AbstractStateT newState = _allStates.Single(state => predicate.Invoke(state));
            Switch(newState);
        }

        public void Add(IEnumerable<AbstractStateT> states) => _allStates.AddRange(states);
        public void Add(AbstractStateT state) => _allStates.Add(state);
        public void Remove(AbstractStateT state) => _allStates.Remove(state);

        // не делать public, т.к. вся информация об общих состояниях должна вноситься при создании экземпляра объекта (Подразумевается, что добавляться состояния будут также сразу после создания объекта)
        private void Switch(AbstractStateT newState)
        {
            if (IsStarted) CurrentState.OnExit();
            else IsStarted = true;

            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}
