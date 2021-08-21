using System;
using Desdiene.StateMachine.State;

namespace Desdiene.StateMachine.StateSwitching
{
    public interface IStateSwitcher<AbstractStateT> where AbstractStateT : IStateEntryExitPoint
    {
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;

        void Switch(Predicate<AbstractStateT> predicate);
    }
}
