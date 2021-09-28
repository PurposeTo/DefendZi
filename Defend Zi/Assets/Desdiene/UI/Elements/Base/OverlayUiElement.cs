using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.UI.Elements
{

    /// <summary>
    /// Класс описывает UI элемент, находящийся на Canvas overlay
    /// </summary>
    // todo добавить валидацию, что самый родительский Canvas имеет свойство overlay
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public abstract class OverlayUiElement : MonoBehaviourExt, IOverlayUiElement
    {
        private string _typeName;
        private string _gameObjectName;
        private ICyclicalProcesses _beforeHide;
        private Canvas _canvas;

        protected sealed override void AwakeExt()
        {
            _typeName = GetType().Name;
            _gameObjectName = gameObject.name;
            _beforeHide = new CyclicalParallelProcesses($"Before hide {_typeName} on \"{_gameObjectName}\"");
            _canvas = GetComponent<Canvas>();
            AwakeElement();
        }

        protected sealed override void OnDestroyExt()
        {
            _beforeHide.Clear();
            OnDestroyElement();
        }

        private bool IsShowing => _canvas.enabled;
        private bool IsHidden => !_canvas.enabled;

        public void Show()
        {
            if (IsHidden)
            {
                Debug.Log($"Show {_typeName} on \"{_gameObjectName}\"");
                EnableCanvas();
                ShowElement();
            }
        }

        public void Hide()
        {
            if (IsShowing)
            {
                Debug.Log($"Hide {_typeName} on \"{_gameObjectName}\"");
                BeforeHidingElement(_beforeHide);
                _beforeHide.WhenStopped += DisableCanvasAndHideElement;
            }
        }

        protected abstract void AwakeElement();
        protected abstract void OnDestroyElement();
        protected abstract void ShowElement();
        protected abstract void BeforeHidingElement(ICyclicalProcessesMutator beforeHide);
        protected abstract void HideElement();

        private void DisableCanvasAndHideElement()
        {
            _beforeHide.WhenStopped -= DisableCanvasAndHideElement;
            _beforeHide.Clear();
            DisableCanvas();
            HideElement();
        }

        private void DisableCanvas() => _canvas.enabled = false;
        private void EnableCanvas() => _canvas.enabled = true;
    }
}
