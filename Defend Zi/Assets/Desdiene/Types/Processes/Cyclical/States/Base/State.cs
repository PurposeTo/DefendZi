using System;
using Desdiene.StateMachines.States;

namespace Desdiene.Types.Processes
{
    public partial class CyclicalProcess
    {
        private abstract class State : IState
        {
            protected State(CyclicalProcess it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IState.OnEnter()
            {
                OnEnter();
            }

            void IState.OnExit()
            {
                OnExit();
            }

            public string Name { get; }
            public abstract bool KeepWaiting { get; }
            protected CyclicalProcess It { get; }

            public abstract Action SubscribeToWhenRunning(Action action, Action value);
            public abstract Action SubscribeToWhenCompleted(Action action, Action value);

            public abstract void Start();

            public abstract void Stop();

            protected virtual void OnEnter() { }

            protected virtual void OnExit() { }

            protected State SwitchState<stateT>() where stateT : State => It.SwitchState<stateT>();
        }
    }
}
