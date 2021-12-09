using UnityEngine;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromDisplayedToHidden : State
        {
            public FromDisplayedToHidden(UiElement it) : base(it) { }

            protected override void OnEnter()
            {
                It.Animation.Hide(() =>
                {
                    It.HideElement();
                    SwitchState<Hidden>();
                });
            }

            protected override void OnExit() { }

            public override void Show()
            {
                void ShowAfterHidden()
                {
                    It.Show();
                    It.WhenHidden -= ShowAfterHidden;
                }
                It.WhenHidden += ShowAfterHidden;
            }

            public override void Hide() { }
        }
    }
}
