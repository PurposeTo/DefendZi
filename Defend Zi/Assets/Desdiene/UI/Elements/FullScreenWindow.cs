using System;
using Desdiene.Types.ProcessContainers;
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

        protected sealed override void ShowElement(Action show)
        {
            _fullScreenWindowsContainer.HideOthers(this);
            ShowWindow(show);
        }

        protected sealed override void HideElement(Action hide) => HideWindow(hide);

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual void ShowWindow(Action show) => show.Invoke();
        protected virtual void HideWindow(Action hide) => hide.Invoke();
    }
}
