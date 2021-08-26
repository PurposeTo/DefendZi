using System.Collections.Generic;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching.Base;
using Desdiene.Types.AtomicReference;

namespace Desdiene.StateMachine.StateSwitching
{
    public class StateSwitcher<AbstractStateT> :
        StateSwitcherBase<AbstractStateT>,
        IStateSwitcher<AbstractStateT>
        where AbstractStateT : class, IStateEntryExitPoint
    {
        public StateSwitcher(IRef<AbstractStateT> refCurrentState)
            : base(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
            : base(allStates, refCurrentState) { }

        protected override AbstractStateT ExitSwitchEnter(AbstractStateT newState)
        {
            if (IsCurrentStateNotNull) CurrentState.OnExit();

            CurrentState = newState;
            CurrentState.OnEnter();
            return CurrentState;
        }
    }


    public class StateSwitcher<AbstractStateT, MutableDataT> :
        StateSwitcherBase<AbstractStateT>,
        IStateSwitcher<AbstractStateT, MutableDataT>
        where AbstractStateT : class, IStateEntryExitPoint<MutableDataT>
        where MutableDataT : class
    {
        public StateSwitcher(IRef<AbstractStateT> refCurrentState)
            : base(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
            : base(allStates, refCurrentState) { }

        protected override AbstractStateT ExitSwitchEnter(AbstractStateT newState)
        {
            if (!IsStateContains(newState))
            {
                throw new System.InvalidOperationException("You need to add the state to all states, before switching");
            }

            MutableDataT mutableData = null;

            if (IsCurrentStateNotNull) mutableData = CurrentState.OnExit();

            CurrentState = newState;
            CurrentState.OnEnter(mutableData);
            return CurrentState;
        }
    }
}
