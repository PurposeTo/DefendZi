using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.States.Base
{
    public abstract class State : IStateEntryExitPoint<StateContext>
    {
        private readonly IStateSwitcher<State, StateContext> _stateSwitcher;

        public State(IStateSwitcher<State, StateContext> stateSwitcher, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            Name = name;
            _stateSwitcher = stateSwitcher;
        }

        protected Action onStarted;
        protected Action onCompleted;
        private Action _onChanged;

        // microsoft docs: don't use abstract/virtual events!
        public event Action OnStarted
        {
            add => SubscribeToOnStarted(value);
            remove => onStarted -= value;
        }

        // microsoft docs: don't use abstract/virtual events!
        public event Action OnCompleted
        {
            add => SubscribeToOnCompleted(value);
            remove => onCompleted -= value;
        }

        public event Action OnChanged
        {
            add => _onChanged += value;
            remove => _onChanged -= value;
        }

        void IStateEntryExitPoint<StateContext>.OnEnter(StateContext stateContext)
        {
            KeepWaiting = this is Running;

            if (stateContext != null)
            {
                onStarted = stateContext.OnStarted;
                onCompleted = stateContext.OnCompleted;
                _onChanged = stateContext.OnChanged;
            }

            OnEnter();
        }

        StateContext IStateEntryExitPoint<StateContext>.OnExit()
        {
            OnExit();
            return new StateContext(onStarted, onCompleted, _onChanged);
        }

        public string Name { get; }
        public bool KeepWaiting { get; private set; }

        public abstract void Start();

        public abstract void Complete();

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        protected abstract void SubscribeToOnStarted(Action value);
        protected abstract void SubscribeToOnCompleted(Action value);

        protected State SwitchState<stateT>() where stateT : State
        {
            bool pastKeepWaiting = KeepWaiting;
            State nextState = _stateSwitcher.Switch<stateT>();
            bool nextKeepWaiting = nextState.KeepWaiting;
            if (pastKeepWaiting != nextKeepWaiting) nextState._onChanged?.Invoke();
            return nextState;
        }
    }
}
