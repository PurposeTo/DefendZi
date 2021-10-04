﻿using Desdiene.Types.ProcessContainers;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает PopUp overlay окно.
    /// 
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// </summary>
    public class PopUpWindow : OverlayUiElement
    {
        protected sealed override void AwakeElement()
        {
            AwakeWindow();
        }

        protected sealed override void OnDestroyElement() => OnDestroyWindow();

        protected sealed override void ShowElement() => ShowWindow();
        protected sealed override void BeforeHidingElement(ICyclicalProcessesMutator beforeHide) => BeforeHidingWindow(beforeHide);
        protected sealed override void HideElement() => HideWindow();

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual void ShowWindow() { }
        protected virtual void BeforeHidingWindow(ICyclicalProcessesMutator beforeHide) { }
        protected virtual void HideWindow() { }
    }
}
