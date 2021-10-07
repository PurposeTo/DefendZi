using System;
using Desdiene.Types.Processes;
using Desdiene.UI.Components;
using Zenject;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает полноэкранное overlay окно.
    /// При открытии остальные полноэкранные окна сворачиваются.
    /// 
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// </summary>
    public class FullScreenWindow : UiElement, IFullScreenWindow
    {
        private FullScreenWindowsContainer _fullScreenWindowsContainer;

        [Inject]
        private void Constructor(FullScreenWindowsContainer fullScreenWindowsContainer)
        {
            _fullScreenWindowsContainer = fullScreenWindowsContainer ?? throw new System.ArgumentNullException(nameof(fullScreenWindowsContainer));
        }

        protected sealed override void AwakeElement()
        {
            _fullScreenWindowsContainer.Add(this);
            AwakeWindow();
        }

        protected sealed override void OnDestroyElement()
        {
            OnDestroyWindow();
            _fullScreenWindowsContainer.Remove(this);
        }

        protected sealed override IProcessAccessorNotifier ShowElement()
        {
            _fullScreenWindowsContainer.HideOthers(this);
           return ShowWindow();
        }

        protected sealed override IProcessAccessorNotifier HideElement() => HideWindow();

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual IProcessAccessorNotifier ShowWindow() => new CompletedProcess();
        protected virtual IProcessAccessorNotifier HideWindow() => new CompletedProcess();
    }
}
