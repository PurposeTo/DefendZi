using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

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
                IProcessAccessorNotifier wait = it.ShowElement();

                void SetDisplayed()
                {
                    wait.WhenCompleted -= SetDisplayed;
                    SwitchState<Displayed>();
                }

                wait.WhenCompleted += SetDisplayed;
            }

            protected override void OnExit(UiElement it) { }

            protected override IProcessAccessorNotifier Show(UiElement it) => this;

            protected override IProcessAccessorNotifier Hide(UiElement it)
            {
                IProcess wait = new LinearProcess("Ожидание открытия и последующего закрытия окна");
                wait.Start();

                void HideAfterDisplayed()
                {
                    IProcessAccessorNotifier waitForHidden = it.Hide();

                    void StopWaiting()
                    {
                        waitForHidden.WhenCompleted -= StopWaiting;
                        wait.Stop();
                    }
                    waitForHidden.WhenCompleted += StopWaiting;

                    it.WhenDisplayed -= HideAfterDisplayed;
                }
                it.WhenDisplayed += HideAfterDisplayed;

                return wait;
            }
        }
    }
}
