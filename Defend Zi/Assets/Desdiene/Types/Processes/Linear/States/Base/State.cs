using System;
using Desdiene.StateMachines.States;

namespace Desdiene.Types.Processes
{
    public partial class OptionalLinearProcess
    {
        private abstract class State : IStateEntryExitPoint
        {
            protected State(OptionalLinearProcess it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter()
            {
                OnEnter();
            }

            void IStateEntryExitPoint.OnExit()
            {
                OnExit();
            }

            public abstract bool KeepWaiting { get; }
            protected OptionalLinearProcess It { get; }

            public abstract Action SubscribeToWhenRunning(Action action, Action value);
            public abstract Action SubscribeToWhenCompleted(Action action, Action value);

            public abstract void Start();
            public abstract void Complete();

            protected virtual void OnEnter() { }

            protected virtual void OnExit() { }

            protected State SwitchState<stateT>() where stateT : State => It.SwitchState<stateT>();
        }
    }
}
