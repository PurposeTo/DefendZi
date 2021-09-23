using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes.Linear.States
{
    public abstract class State : IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly StateContext _stateContext;

        public State(IStateSwitcher<State> stateSwitcher, StateContext stateContext, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _stateContext = stateContext ?? throw new ArgumentNullException(nameof(stateContext));
            Name = name;
        }

        // microsoft docs: don't use abstract/virtual events!
        public event Action OnStarted
        {
            add
            {
                _stateContext.OnStarted = SubscribeToOnStarted(_stateContext.OnStarted, value);
            }
            remove => _stateContext.OnStarted -= value;
        }

        // microsoft docs: don't use abstract/virtual events!
        public event Action OnCompleted
        {
            add => _stateContext.OnCompleted = SubscribeToOnCompleted(_stateContext.OnCompleted, value);
            remove => _stateContext.OnCompleted -= value;
        }

        public event Action OnChanged
        {
            add => _stateContext.OnChanged += value;
            remove => _stateContext.OnChanged -= value;
        }

        void IStateEntryExitPoint.OnEnter()
        {
            OnEnter();
        }

        void IStateEntryExitPoint.OnExit()
        {
            OnExit();
        }

        public string Name { get; }
        public abstract bool KeepWaiting { get; }
        protected StateContext StateContext => _stateContext;

        public abstract void Start();

        public abstract void Complete();

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        protected abstract Action SubscribeToOnStarted(Action onStarted, Action value);
        protected abstract Action SubscribeToOnCompleted(Action onCompleted, Action value);

        protected State SwitchState<stateT>() where stateT : State
        {
            bool pastKeepWaiting = KeepWaiting;
            State nextState = _stateSwitcher.Switch<stateT>();
            bool nextKeepWaiting = nextState.KeepWaiting;
            if (pastKeepWaiting != nextKeepWaiting) nextState.InvokeOnChanged();
            return nextState;
        }

        private void InvokeOnChanged() => _stateContext.OnChanged?.Invoke();
    }
}
