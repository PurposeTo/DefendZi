using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.ProcessContainers;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromHiddenToDisplayed : State
        {
            public FromHiddenToDisplayed(UiElement _it, IStateSwitcher<State, UiElement> stateSwitcher)
                : base(_it, stateSwitcher)
            {

            }

            protected override void OnEnter(UiElement it)
            {
                it.EnableCanvas();
                it.ShowElement(() => SwitchState<Displayed>());
            }

            protected override void OnExit(UiElement it) { }

            protected override void Show(UiElement it) { }

            protected override void Hide(UiElement it)
            {
                void HideAfterDisplayed()
                {
                    it.Hide();
                    it.WhenDisplayed -= HideAfterDisplayed;
                }
                it.WhenDisplayed += HideAfterDisplayed;
            }
        }
    }
}
