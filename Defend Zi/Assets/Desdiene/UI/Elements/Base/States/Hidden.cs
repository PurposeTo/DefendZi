using System;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class Hidden : State
        {
            public Hidden(UiElement _it, IStateSwitcher<State, UiElement> stateSwitcher)
                : base(_it, stateSwitcher) { }


            public override Action SubscribeToWhenHidden(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenHidden(action, value);
            }

            protected override void OnEnter(UiElement it)
            {
                it.DisableCanvas();
                it.HideElement();
                it.whenHidden?.Invoke();
            }

            protected override void OnExit(UiElement it) { }

            protected override void Show(UiElement it)
            {
                Debug.Log($"Show {it._typeName} on \"{it._gameObjectName}\"");
                SwitchState<FromHiddenToDisplayed>();
            }

            protected override void Hide(UiElement it) { }
        }
    }
}
