using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает PopUp overlay окно.
    /// </summary>
    public class PopUpWindow : OverlayUiElement
    {
        protected sealed override void AwakeElement()
        {
            AwakeWindow();
        }

        protected sealed override void OnDisableElement() => OnDisableWindow();

        protected sealed override void ShowElement() => ShowWindow();
        protected sealed override void HideElement() => HideWindow();

        protected virtual void AwakeWindow() { }
        protected virtual void OnDisableWindow() { }

        protected virtual void ShowWindow() { }
        protected virtual void HideWindow() { }
    }
}
