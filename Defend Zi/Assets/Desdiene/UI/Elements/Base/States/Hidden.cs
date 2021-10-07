using System;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;
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
                it.whenHidden?.Invoke();
            }

            protected override void OnExit(UiElement it) { }

            protected override IProcessAccessorNotifier Show(UiElement it)
            {
                Debug.Log($"Show {it._typeName} on \"{it._gameObjectName}\"");
                return SwitchState<FromHiddenToDisplayed>();
            }

            protected override IProcessAccessorNotifier Hide(UiElement it) => new CompletedProcess();
        }
    }
}
