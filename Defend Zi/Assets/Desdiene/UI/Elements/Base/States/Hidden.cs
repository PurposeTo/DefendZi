using System;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class Hidden : State
        {
            public Hidden(UiElement _it) : base(_it) { }

            public override Action SubscribeToWhenHidden(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenHidden(action, value);
            }

            protected override void OnEnter()
            {
                It.DisableInteractible();
                It.DisableCanvas();
                Debug.Log($"{It._typeName} on \"{It._gameObjectName}\" is hidden");
                It.whenHidden?.Invoke();
            }

            protected override void OnExit() { }

            public override void Show()
            {
                Debug.Log($"Start to show {It._typeName} on \"{It._gameObjectName}\"");
                SwitchState<FromHiddenToDisplayed>();
            }

            public override void Hide() { }
        }
    }
}
