using System;
using Desdiene.StateMachines.States;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private abstract class State : IStateEntryExitPoint
        {
            private readonly IProcess _stateExecuting;

            protected State(UiElement it)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
                _stateExecuting = new CyclicalProcess($"Выполнение состояния {GetType()}");
            }

            void IStateEntryExitPoint.OnEnter()
            {
                _stateExecuting.Start();
                OnEnter();
            }

            void IStateEntryExitPoint.OnExit()
            {
                OnExit();
                _stateExecuting.Stop();
            }

            protected UiElement It { get; }

            public virtual Action SubscribeToWhenDisplayed(Action action, Action value) => action += value;
            public virtual Action SubscribeToWhenHidden(Action action, Action value) => action += value;

            public abstract void Show();
            public abstract void Hide();

            protected abstract void OnEnter();
            protected abstract void OnExit();

            protected State SwitchState<stateT>() where stateT : State => It._stateSwitcher.Switch<stateT>();
        }
    }
}
