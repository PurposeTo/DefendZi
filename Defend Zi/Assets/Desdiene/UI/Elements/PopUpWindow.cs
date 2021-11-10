namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает PopUp overlay окно.
    /// </summary>
    public abstract class PopUpWindow : UiElement
    {
        protected sealed override void AwakeElement()
        {
            AwakeWindow();
        }

        protected sealed override void OnDestroyElement() => OnDestroyWindow();

        protected sealed override void ShowElement() => ShowWindow();
        protected sealed override void HideElement() => HideWindow();

        protected abstract void AwakeWindow();
        protected abstract void OnDestroyWindow();

        protected abstract void ShowWindow();
        protected abstract void HideWindow();
    }
}
