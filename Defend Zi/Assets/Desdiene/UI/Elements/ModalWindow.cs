using UnityEngine;
using UnityEngine.UI;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Описывает модальное overlay окно.
    /// На том же объекте, что и данный скрипт, должен висеть полноэкранный Image, блокирующий raycast других окон.
    /// Само тело модального окна (т.к. вероятно оно будет меньше, чем во весь экран) должно быть дочерним элементом.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public abstract class ModalWindow : UiElement
    {
        private Image _image;

        protected sealed override void AwakeElement()
        {
            _image = GetComponent<Image>();
            ValidateRaycastBlocker();
            AwakeWindow();
        }

        protected sealed override void OnDestroyElement() => OnDestroyWindow();

        protected sealed override void ShowElement() => ShowWindow();
        protected sealed override void HideElement() => HideWindow();

        protected virtual void AwakeWindow() { }
        protected virtual void OnDestroyWindow() { }

        protected virtual void ShowWindow() { }
        protected virtual void HideWindow() { }

        private void ValidateRaycastBlocker()
        {
            if (!TryGetComponent(out CanvasScaler canvasScaler))
            {
                ValidateRectTransform();
            }
            ValidateRaycastTarget();
        }

        private void ValidateRectTransform()
        {
            if (RectTransform.anchorMin != Vector2.zero)
            {
                Debug.LogWarning($"Set default anchorMin to raycast blocker on {name}");
                RectTransform.anchorMin = Vector2.zero;
            }
            if (RectTransform.anchorMax != Vector2.one)
            {
                Debug.LogWarning($"Set default anchorMax to raycast blocker on {name}");
                RectTransform.anchorMax = Vector2.one;
            }
            if (RectTransform.offsetMin != Vector2.zero)
            {
                Debug.LogWarning($"Set default offsetMin to raycast blocker on {name}");
                RectTransform.offsetMin = Vector2.zero;
            }
            if (RectTransform.offsetMax != Vector2.zero)
            {
                Debug.LogWarning($"Set default offsetMax to raycast blocker on {name}");
                RectTransform.offsetMax = Vector2.zero;
            }
            Vector2 middleVector = new Vector2(0.5f, 0.5f);
            if (RectTransform.pivot != middleVector)
            {
                Debug.LogWarning($"Set default pivot to raycast blocker on {name}");
                RectTransform.pivot = middleVector;
            }
            if (RectTransform.rotation != Quaternion.identity)
            {
                Debug.LogWarning($"Set default rotation to raycast blocker on {name}");
                RectTransform.rotation = Quaternion.identity;
            }
        }

        private void ValidateRaycastTarget()
        {
            if (GetComponentsInParent<GraphicRaycaster>().Length == 0)
            {
                Debug.LogWarning($"Added GraphicRaycaster on {name}");
                gameObject.AddComponent<GraphicRaycaster>();
            }

            if (!_image.raycastTarget)
            {
                Debug.LogWarning($"Set [raycastTarget = true] to raycast blocker on {name}");
                _image.raycastTarget = true;
            }
        }
    }
}
