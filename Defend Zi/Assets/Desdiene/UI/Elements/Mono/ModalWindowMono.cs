using Desdiene.UI.Animators;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// Требует компонент IUiElementAnimation.
    /// </summary>
    [RequireComponent(typeof(IUiElementAnimation))]
    public class ModalWindowMono : ModalWindow
    {
        private IUiElementAnimation _animation;
        
        protected override void AwakeWindow()
        {
            _animation = GetComponent<IUiElementAnimation>();
        }

        protected override IUiElementAnimation Animation => _animation;
        
        protected override void HideWindow() { }
        protected override void OnDestroyWindow() { }
        protected override void ShowWindow() { }
    }
}
