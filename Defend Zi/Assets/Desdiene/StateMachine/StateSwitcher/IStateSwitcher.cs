namespace Desdiene.StateMachine.StateSwitcher
{
    public interface IStateSwitcher<AbstractStateT> where AbstractStateT : class
    {
        void Switch<ConcreteStateT>() where ConcreteStateT : AbstractStateT;
    }
}
