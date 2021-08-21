namespace Desdiene.StateMachine.StateSwitcher
{
    public interface IStateSwitcher<AbstractStateT> where AbstractStateT : IStateEntryExitPoint
    {
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;
    }
}
