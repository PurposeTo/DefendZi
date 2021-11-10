using Desdiene.UI.Components;
using UnityEngine;
using Zenject;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// Требует компонент IUiElementAnimation.
    /// </summary>
    [RequireComponent(typeof(IUiElementAnimation))]
    public class FullScreenWindowMono : FullScreenWindow, IFullScreenWindow
    {
        private FullScreenWindowsContainer _fullScreenWindowsContainer;
        private IUiElementAnimation _animation;

        [Inject]
        private void Constructor(FullScreenWindowsContainer fullScreenWindowsContainer)
        {
            _fullScreenWindowsContainer = fullScreenWindowsContainer ?? throw new System.ArgumentNullException(nameof(fullScreenWindowsContainer));
        }

        protected override void AwakeWindow()
        {
            _animation = GetInitedComponent<IUiElementAnimation>();
        }

        protected override IUiElementAnimation Animation => _animation;

        protected override void HideWindow() { }
        protected override void OnDestroyWindow() { }
        protected override void ShowWindow() { }
    }
}
