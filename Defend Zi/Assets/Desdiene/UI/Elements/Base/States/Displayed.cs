using System;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class Displayed : State
        {
            public Displayed(UiElement _it) : base(_it) { }

            public override Action SubscribeToWhenDisplayed(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenDisplayed(action, value);
            }

            protected override void OnEnter()
            {
                Debug.Log($"{It._typeName} on \"{It._gameObjectName}\" is displayed");
                It.whenDisplayed?.Invoke();
            }

            protected override void OnExit() { }

            public override void Show() { }

            public override void Hide()
            {
                Debug.Log($"Start to hide {It._typeName} on \"{It._gameObjectName}\"");
                SwitchState<FromDisplayedToHidden>();
            }
        }
    }
}
