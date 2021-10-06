using System;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class Displayed : State
        {
            public Displayed(UiElement _it, IStateSwitcher<State, UiElement> stateSwitcher)
                : base(_it, stateSwitcher) { }


            public override Action SubscribeToWhenDisplayed(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenDisplayed(action, value);
            }

            protected override void OnEnter(UiElement it)
            {
                it.whenDisplayed?.Invoke();
            }
            protected override void OnExit(UiElement it) { }

            protected override void Show(UiElement it) { }

            protected override void Hide(UiElement it)
            {
                Debug.Log($"Hide {it._typeName} on \"{it._gameObjectName}\"");
                SwitchState<FromDisplayedToHidden>();
            }
        }
    }
}
