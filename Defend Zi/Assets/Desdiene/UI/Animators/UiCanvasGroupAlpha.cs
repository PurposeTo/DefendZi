using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.UI.Animators
{
    public class UiCanvasGroupAlpha : MonoBehaviourExtContainer, IUiElementAnimation
    {
        private const float _transparentAlpha = 0f;
        private const float _displayedAlpha = 1f;
        private readonly UpdateActionType.Mode _updatingMode;
        private readonly CanvasGroup _canvasGroup;
        private readonly float _animationTime;
        private ICoroutine _animation;

        public UiCanvasGroupAlpha(MonoBehaviourExt mono,
                                  CanvasGroup canvasGroup,
                                  UpdateActionType.Mode updatingMode,
                                  float animationTime) : base(mono)
        {
            _canvasGroup = canvasGroup != null
                ? canvasGroup
                : throw new ArgumentNullException(nameof(canvasGroup));

            _updatingMode = updatingMode;

            _animation = new CoroutineWrap(mono);
            _animationTime = animationTime;
        }

        void IUiElementAnimation.Show(Action OnEnded)
        {
            _animation.ReStart(ToDisplayed(OnEnded));
        }

        void IUiElementAnimation.Hide(Action OnEnded)
        {
            _animation.ReStart(ToHidden(OnEnded));
        }

        private float Alpha { get => _canvasGroup.alpha; set { _canvasGroup.alpha = value; } }

        private IEnumerator ToHidden(Action OnEnded)
        {
            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => Alpha > _transparentAlpha, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                Alpha -= delta;
            });

            SetDisplayed();
            yield return _animation.StartNested(enumerator);
            SetHidden();
            OnEnded?.Invoke();
        }

        private IEnumerator ToDisplayed(Action OnEnded)
        {
            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => Alpha < _displayedAlpha, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                Alpha += delta;
            });

            SetHidden();
            yield return _animation.StartNested(enumerator);
            SetDisplayed();
            OnEnded?.Invoke();
        }

        private void SetDisplayed()
        {
            Alpha = _displayedAlpha;
        }

        private void SetHidden()
        {
            Alpha = _transparentAlpha;
        }
    }
}
