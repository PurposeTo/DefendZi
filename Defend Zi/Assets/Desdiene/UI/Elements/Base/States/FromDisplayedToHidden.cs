using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromDisplayedToHidden : State
        {
            public FromDisplayedToHidden(UiElement _it, IStateSwitcher<State, UiElement> stateSwitcher)
                : base(_it, stateSwitcher) { }

            protected override void OnEnter(UiElement it)
            {
                SwitchState<Hidden>();
            }

            protected override void OnExit(UiElement it) { }

            protected override void Show(UiElement it)
            {
                void ShowAfterHidden()
                {
                    it.Hide();
                    it.WhenHidden -= ShowAfterHidden;
                }
                it.WhenHidden += ShowAfterHidden;
            }

            protected override void Hide(UiElement it) { }
        }
    }
}
