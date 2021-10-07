using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.ProcessContainers;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromDisplayedToHidden : State
        {
            public FromDisplayedToHidden(UiElement _it, IStateSwitcher<State, UiElement> stateSwitcher)
                : base(_it, stateSwitcher)
            {

            }

            protected override void OnEnter(UiElement it)
            {
                it.HideElement(() => SwitchState<Hidden>());
            }

            protected override void OnExit(UiElement it) { }

            protected override void Show(UiElement it)
            {
                void ShowAfterHidden()
                {
                    it.Show();
                    it.WhenHidden -= ShowAfterHidden;
                }
                it.WhenHidden += ShowAfterHidden;
            }

            protected override void Hide(UiElement it) { }
        }
    }
}
