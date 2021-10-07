using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private abstract class State : IStateEntryExitPoint<UiElement>
        {
            private readonly IStateSwitcher<State, UiElement> _stateSwitcher;
            private readonly UiElement _it;

            protected State(UiElement it, IStateSwitcher<State, UiElement> stateSwitcher)
            {
                _it = it ?? throw new ArgumentNullException(nameof(it));
                _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            }

            void IStateEntryExitPoint<UiElement>.OnEnter(UiElement it)
            {
                OnEnter(it);
            }

            void IStateEntryExitPoint<UiElement>.OnExit(UiElement it)
            {
                OnExit(it);
            }

            public virtual Action SubscribeToWhenDisplayed(Action action, Action value) => action += value;
            public virtual Action SubscribeToWhenHidden(Action action, Action value) => action += value;

            public void Show() => Show(_it);
            public void Hide() => Hide(_it);

            protected abstract void Show(UiElement it);
            protected abstract void Hide(UiElement it);

            protected abstract void OnEnter(UiElement it);
            protected abstract void OnExit(UiElement it);

            protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
