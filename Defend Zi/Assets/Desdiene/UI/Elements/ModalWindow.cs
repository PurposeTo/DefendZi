using Desdiene.Types.ProcessContainers;
using UnityEngine;
using UnityEngine.UI;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает модальное overlay окно.
    /// На том же объекте, что и данный скрипт, должен висеть полноэкранный Image, блокирующий raycast других окон.
    /// Само тело модального окна (т.к. вероятно оно будет меньше, чем во весь экран) должно быть дочерним элементом.
    /// 
    /// Скрипт может быть повешан на объект для логического обозначения.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ModalWindow : OverlayUiElement
    {
        private RectTransform _rectTransform;
        private Image _image;

        protected sealed override void AwakeElement()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            ValidateRaycastBlocker();
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

        private void ValidateRaycastBlocker()
        {
            ValidateRectTransform();
            ValidateImage();
        }

        private void ValidateRectTransform()
        {
            if (_rectTransform.anchorMin != Vector2.zero)
            {
                Debug.LogWarning($"Set default anchorMin to raycast blocker on {name}");
                _rectTransform.anchorMin = Vector2.zero;
            }
            if (_rectTransform.anchorMax != Vector2.one)
            {
                Debug.LogWarning($"Set default anchorMax to raycast blocker on {name}");
                _rectTransform.anchorMax = Vector2.one;
            }
            if (_rectTransform.offsetMin != Vector2.zero)
            {
                Debug.LogWarning($"Set default offsetMin to raycast blocker on {name}");
                _rectTransform.offsetMin = Vector2.zero;
            }
            if (_rectTransform.offsetMax != Vector2.zero)
            {
                Debug.LogWarning($"Set default offsetMax to raycast blocker on {name}");
                _rectTransform.offsetMax = Vector2.zero;
            }
            Vector2 middleVector = new Vector2(0.5f, 0.5f);
            if (_rectTransform.pivot != middleVector)
            {
                Debug.LogWarning($"Set default pivot to raycast blocker on {name}");
                _rectTransform.pivot = middleVector;
            }
            if (_rectTransform.rotation != Quaternion.identity)
            {
                Debug.LogWarning($"Set default rotation to raycast blocker on {name}");
                _rectTransform.rotation = Quaternion.identity;
            }
        }

        private void ValidateImage()
        {
            if (!_image.raycastTarget)
            {
                Debug.LogWarning($"Set [raycastTarget = true] to raycast blocker on {name}");
                _image.raycastTarget = true;
            }
        }
    }
}
