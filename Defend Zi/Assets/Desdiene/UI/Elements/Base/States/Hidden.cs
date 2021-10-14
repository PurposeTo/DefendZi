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
            public Hidden(UiElement _it, IStateSwitcher<State> stateSwitcher)
                : base(_it, stateSwitcher) { }


            public override Action SubscribeToWhenHidden(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenHidden(action, value);
            }

            protected override void OnEnter()
            {
                It.DisableCanvas();
                It.whenHidden?.Invoke();
            }

            protected override void OnExit() { }

            public override IProcessAccessorNotifier Show()
            {
                Debug.Log($"Show {It._typeName} on \"{It._gameObjectName}\"");
                return SwitchState<FromHiddenToDisplayed>();
            }

            public override IProcessAccessorNotifier Hide() => new CompletedProcess();
        }
    }
}
