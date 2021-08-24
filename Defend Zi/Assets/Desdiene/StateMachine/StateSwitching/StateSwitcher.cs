using System.Collections.Generic;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching.Base;
using Desdiene.Types.AtomicReference;

namespace Desdiene.StateMachine.StateSwitching
{
    public class StateSwitcher<AbstractStateT> :
        StateSwitcherBase<AbstractStateT>,
        IStateSwitcher<AbstractStateT>
        where AbstractStateT : IStateEntryExitPoint
    {
        public StateSwitcher(IRef<AbstractStateT> refCurrentState)
            : base(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
            : base(allStates, refCurrentState) { }

        protected override void Switch(AbstractStateT newState)
        {
            if (IsStarted) CurrentState.OnExit();
            else IsStarted = true;

            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }


    public class StateSwitcher<AbstractStateT, MutableDataT> :
        StateSwitcherBase<AbstractStateT>,
        IStateSwitcher<AbstractStateT, MutableDataT>
        where AbstractStateT : IStateEntryExitPoint<MutableDataT>
        where MutableDataT : class
    {
        public StateSwitcher(IRef<AbstractStateT> refCurrentState)
            : base(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
            : base(allStates, refCurrentState) { }

        protected override void Switch(AbstractStateT newState)
        {
            MutableDataT mutableData = null;
            if (IsStarted)
            {
                mutableData = CurrentState.OnExit();
            }
            else IsStarted = true;

            CurrentState = newState;
            CurrentState.OnEnter(mutableData);
        }
    }
}
