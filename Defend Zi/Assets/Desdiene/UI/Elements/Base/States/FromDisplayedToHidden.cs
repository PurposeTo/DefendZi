using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private sealed class FromDisplayedToHidden : State
        {
            public FromDisplayedToHidden(UiElement _it, IStateSwitcher<State> stateSwitcher)
                : base(_it, stateSwitcher)
            {

            }

            protected override void OnEnter()
            {
                IProcessAccessorNotifier wait = It.HideElement();

                void SetHidden()
                {
                    wait.WhenCompleted -= SetHidden;
                    SwitchState<Hidden>();
                }

                wait.WhenCompleted += SetHidden;
            }

            protected override void OnExit() { }

            public override IProcessAccessorNotifier Show()
            {
                IProcess wait = new LinearProcess("Ожидание закрытия и последующего открытия окна");
                wait.Start();

                void ShowAfterHidden()
                {
                    IProcessAccessorNotifier waitForDisplayed = It.Show();

                    void StopWaiting()
                    {
                        waitForDisplayed.WhenCompleted -= StopWaiting;
                        wait.Stop();
                    }
                    waitForDisplayed.WhenCompleted += StopWaiting;
                    It.WhenHidden -= ShowAfterHidden;
                }
                It.WhenHidden += ShowAfterHidden;
                return wait;
            }

            public override IProcessAccessorNotifier Hide() => this;
        }
    }
}
