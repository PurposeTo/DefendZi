using System;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает PopUp overlay окно.
    /// 
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// </summary>
    public class PopUpWindow : UiElement
    {
        protected sealed override void AwakeElement()
        {
            AwakeWindow();
        }

        protected sealed override void OnDestroyElement() => OnDestroyWindow();

        protected sealed override void ShowElement(Action show) => ShowWindow(show);
        protected sealed override void HideElement(Action hide) => HideWindow(hide);

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual void ShowWindow(Action show) => show.Invoke();
        protected virtual void HideWindow(Action hide) => hide.Invoke();
    }
}
