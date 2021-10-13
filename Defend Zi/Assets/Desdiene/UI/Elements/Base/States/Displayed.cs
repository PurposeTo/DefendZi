using System;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class Displayed : State
        {
            public Displayed(UiElement _it, IStateSwitcher<State> stateSwitcher)
                : base(_it, stateSwitcher) { }


            public override Action SubscribeToWhenDisplayed(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenDisplayed(action, value);
            }

            protected override void OnEnter()
            {
                It.whenDisplayed?.Invoke();
            }
            protected override void OnExit() { }

            public override IProcessAccessorNotifier Show() => new CompletedProcess();

            public override IProcessAccessorNotifier Hide()
            {
                Debug.Log($"Hide {It._typeName} on \"{It._gameObjectName}\"");
                return SwitchState<FromDisplayedToHidden>();
            }
        }
    }
}
