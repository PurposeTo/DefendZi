using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;
using UnityToggle = UnityEngine.UI.Toggle;

namespace Desdiene.UI.Elements
{
    // назван с префиксом "UI" дабы избежать конфликта имен
    [RequireComponent(typeof(UnityToggle))]
    [RequireComponent(typeof(Image))] // component image, который определяет интерактивную область элемента
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class UiToggle : MonoBehaviourExt
    {
        [SerializeField, NotNull] private Sprite _enabledSprite;
        [SerializeField, NotNull] private Sprite _disabledSprite;
        // component image, который определяет изменяемое изображение элемента. Может быть как интерактивной областью, так и нет.
        [SerializeField, NotNull] private Image _image;
        private UnityToggle _unityToggle;

        protected override void AwakeExt()
        {
            _unityToggle = GetComponent<UnityToggle>();
            SetDefaultSettings();
            ChangeImage(_unityToggle.isOn);
            _unityToggle.onValueChanged.AddListener((value) => OnChanged?.Invoke(value));
            SubscribeEvents();
        }

        protected override void OnDestroyExt()
        {
            UnsubscribeEvents();
        }

        public event Action<bool> OnChanged;
        public bool IsOn => _unityToggle.isOn;

        public void SetState(bool enabled)
        {
            if (_unityToggle.isOn != enabled)
            {
                _unityToggle.isOn = enabled;
                // _unityToggle.onValueChanged уже отслеживает изменение
            }
        }

        private void ChangeImage(bool enabled)
        {
            _image.sprite = enabled
                ? _enabledSprite
                : _disabledSprite;
        }

        private void SubscribeEvents()
        {
            OnChanged += ChangeImage;
        }

        private void UnsubscribeEvents()
        {
            OnChanged -= ChangeImage;
        }

        private void SetDefaultSettings()
        {
            var defaultTransition = Selectable.Transition.None;
            if (_unityToggle.transition != defaultTransition)
            {
                Debug.LogWarning($"Set default transition to togle on {name}");
                _unityToggle.transition = defaultTransition;
            }

            var defaultToggleTransition = UnityToggle.ToggleTransition.None;
            if (_unityToggle.toggleTransition != defaultToggleTransition)
            {
                Debug.LogWarning($"Set default toggle transition to togle on {name}");
                _unityToggle.toggleTransition = defaultToggleTransition;
            }
        }
    }
}