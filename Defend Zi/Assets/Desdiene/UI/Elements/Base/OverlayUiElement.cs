using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.UI.Elements
{

    /// <summary>
    /// Класс описывает UI элемент, находящийся на Canvas overlay
    /// </summary>
    // todo добавить валидацию, что самый родительский Canvas имеет свойство overlay
    [RequireComponent(typeof(Canvas))]
    public class OverlayUiElement : MonoBehaviourExt
    {
        private Canvas _canvas;

        protected sealed override void AwakeExt()
        {
            _canvas = GetComponent<Canvas>();
            AwakeElement();
        }

        protected sealed override void OnDisableExt()
        {
            OnDisableElement();
        }

        public void Show()
        {
            _canvas.enabled = true;
            ShowElement();
        }

        public void Hide()
        {
            HideElement();
            _canvas.enabled = false;
        }

        protected virtual void AwakeElement() { }
        protected virtual void OnDisableElement() { }
        protected virtual void ShowElement() { }
        protected virtual void HideElement() { }
    }
}
