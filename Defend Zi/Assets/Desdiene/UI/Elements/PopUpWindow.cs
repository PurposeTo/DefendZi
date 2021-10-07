using System;
using Desdiene.Types.Processes;

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

        protected sealed override IProcessAccessorNotifier ShowElement() => ShowWindow();
        protected sealed override IProcessAccessorNotifier HideElement() => HideWindow();

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual IProcessAccessorNotifier ShowWindow() => new CompletedProcess();
        protected virtual IProcessAccessorNotifier HideWindow() => new CompletedProcess();
    }
}
