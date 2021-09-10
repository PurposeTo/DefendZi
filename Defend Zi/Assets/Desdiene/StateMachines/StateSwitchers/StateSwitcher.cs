using System.Collections.Generic;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers.Base;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.StateMachines.StateSwitchers
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
}
