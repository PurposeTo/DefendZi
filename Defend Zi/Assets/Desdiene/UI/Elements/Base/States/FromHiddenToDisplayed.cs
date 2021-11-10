namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromHiddenToDisplayed : State
        {
            public FromHiddenToDisplayed(UiElement _it) : base(_it) { }

            protected override void OnEnter()
            {
                It.EnableCanvas();
                It.EnableInteractible();
                It.ShowElement();
                It.Animation.Show(() =>
                {
                    SwitchState<Displayed>();
                });
            }

            protected override void OnExit() { }

            public override void Show() { }

            public override void Hide()
            {
                void HideAfterDisplayed()
                {
                    It.Hide();
                    It.WhenDisplayed -= HideAfterDisplayed;
                }
                It.WhenDisplayed += HideAfterDisplayed;
            }
        }
    }
}
