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

        public void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT
        {
            var state = _allStates.Single(it => it is ConcreteStateT);

            if (IsStarted) CurrentState.OnExit();
            else IsStarted = true;

            _refCurrentState.Set(state);
            CurrentState.OnEnter();
        }

        public void Add(IEnumerable<AbstractStateT> states) => _allStates.AddRange(states);
        public void Add(AbstractStateT state) => _allStates.Add(state);
        public void Remove(AbstractStateT state) => _allStates.Remove(state);

        private AbstractStateT CurrentState => _refCurrentState.Get() ?? throw new ArgumentNullException(nameof(CurrentState));
    }
}
