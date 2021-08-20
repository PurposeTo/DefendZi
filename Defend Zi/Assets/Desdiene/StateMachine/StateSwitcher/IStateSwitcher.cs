namespace Desdiene.StateMachine.StateSwitcher
{
    public interface IStateSwitcher<AbstractStateT> where AbstractStateT : IState
    {
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;
    }
}
