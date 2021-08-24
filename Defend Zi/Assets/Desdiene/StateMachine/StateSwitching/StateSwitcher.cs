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


    public class StateSwitcher<AbstractStateT, DynamicDataT> :
        StateSwitcherBase<AbstractStateT>,
        IStateSwitcher<AbstractStateT, DynamicDataT>
        where AbstractStateT : IStateEntryExitPoint<DynamicDataT>
        where DynamicDataT : class
    {
        public StateSwitcher(IRef<AbstractStateT> refCurrentState)
            : base(new List<AbstractStateT>(), refCurrentState) { }

        public StateSwitcher(List<AbstractStateT> allStates, IRef<AbstractStateT> refCurrentState)
            : base(allStates, refCurrentState) { }

        protected override void Switch(AbstractStateT newState)
        {
            DynamicDataT dynamicData = null;
            if (IsStarted)
            {
                dynamicData = CurrentState.OnExit();
            }
            else IsStarted = true;

            CurrentState = newState;
            CurrentState.OnEnter(dynamicData);
        }
    }
}
