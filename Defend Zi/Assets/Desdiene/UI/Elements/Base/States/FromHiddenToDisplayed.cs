using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromHiddenToDisplayed : State
        {
            public FromHiddenToDisplayed(UiElement _it, IStateSwitcher<State> stateSwitcher)
                : base(_it, stateSwitcher)
            {

            }

            protected override void OnEnter()
            {
                It.EnableCanvas();
                IProcessAccessorNotifier wait = It.ShowElement();

                void SetDisplayed()
                {
                    wait.WhenCompleted -= SetDisplayed;
                    SwitchState<Displayed>();
                }

                wait.WhenCompleted += SetDisplayed;
            }

            protected override void OnExit() { }

            public override IProcessAccessorNotifier Show() => this;

            public override IProcessAccessorNotifier Hide()
            {
                IProcess wait = new LinearProcess("Ожидание открытия и последующего закрытия окна");
                wait.Start();

                void HideAfterDisplayed()
                {
                    IProcessAccessorNotifier waitForHidden = It.Hide();

                    void StopWaiting()
                    {
                        waitForHidden.WhenCompleted -= StopWaiting;
                        wait.Stop();
                    }
                    waitForHidden.WhenCompleted += StopWaiting;

                    It.WhenDisplayed -= HideAfterDisplayed;
                }
                It.WhenDisplayed += HideAfterDisplayed;

                return wait;
            }
        }
    }
}
