using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

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
                var wait = it.HideElement();

                void SetHidden()
                {
                    wait.WhenCompleted -= SetHidden;
                    SwitchState<Hidden>();
                }

                wait.WhenCompleted += SetHidden;
            }

            protected override void OnExit(UiElement it) { }

            protected override IProcessAccessorNotifier Show(UiElement it)
            {
                IProcess wait = new LinearProcess("Ожидание закрытия и последующего открытия окна");
                wait.Start();

                void ShowAfterHidden()
                {
                    IProcessAccessorNotifier waitForDisplayed = it.Show();

                    void StopWaiting()
                    {
                        waitForDisplayed.WhenCompleted -= StopWaiting;
                        wait.Stop();
                    }
                    waitForDisplayed.WhenCompleted += StopWaiting;
                    it.WhenHidden -= ShowAfterHidden;
                }
                it.WhenHidden += ShowAfterHidden;
                return wait;
            }

            protected override IProcessAccessorNotifier Hide(UiElement it) => this;
        }
    }
}
