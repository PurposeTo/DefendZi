﻿using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.StateMachines.States;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.StateMachines.StateSwitchers
{
    public class StateSwitcherWithContext<AbstractStateT, StateContextT> :
        IStateSwitcher<AbstractStateT, StateContextT>
        where AbstractStateT : class, IStateEntryExitPoint<StateContextT>
    {
        private readonly StateContextT _it;
        private readonly List<AbstractStateT> _allStates;
        private readonly IRef<AbstractStateT> _refCurrentState;

        public StateSwitcherWithContext(StateContextT it, IRef<AbstractStateT> refCurrentState)
            : this(it, new List<AbstractStateT>(), refCurrentState)
        { }

        public StateSwitcherWithContext(StateContextT it, List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
        {
            _it = it ?? throw new ArgumentNullException(nameof(it));
            _allStates = allStates ?? throw new ArgumentNullException(nameof(allStates));
            _refCurrentState = refCurrentState ?? throw new ArgumentNullException(nameof(refCurrentState));
        }

        /// <summary>
        /// Был ли первоначальное включение состояния?
        /// Не реализован паттерн состояния, т.к. контексты бы пересекались и код был бы нечитабельным.
        /// </summary>
        protected bool IsCurrentStateNotNull => _refCurrentState.Value != null;
        protected AbstractStateT CurrentState
        {
            get
            {
                return _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));
            }
            set
            {
                _refCurrentState.Set(value);
            }
        }

        protected bool IsStateContains(AbstractStateT state) => _allStates.Contains(state);

        /// <summary>
        /// Сменить состояние на указанное по типу.
        /// </summary>
        /// <typeparam name="ConcreteStateT">Тип искомого состояния.</typeparam>
        /// <returns>Новое состояние.</returns>
        public AbstractStateT Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT
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
        public AbstractStateT Switch(Predicate<AbstractStateT> predicate)
        {
            AbstractStateT newState = _allStates.Single(state => predicate.Invoke(state));
            return Switch(newState);
        }

        public bool Any(Predicate<AbstractStateT> predicate) => _allStates.Exists(predicate);

        public void Add(IEnumerable<AbstractStateT> states) => _allStates.AddRange(states);
        public void Add(AbstractStateT state, params AbstractStateT[] states)
        {
            _allStates.Add(state);
            Add(states);
        }

        public void Remove(AbstractStateT state) => _allStates.Remove(state);

        public virtual AbstractStateT Switch(AbstractStateT newState)
        {
            if (!IsStateContains(newState))
            {
                throw new InvalidOperationException("You need to add the state to all states, before switching");
            }

            if (IsCurrentStateNotNull) CurrentState.OnExit(_it);

            CurrentState = newState;
            CurrentState.OnEnter(_it);
            return CurrentState;
        }
    }
}