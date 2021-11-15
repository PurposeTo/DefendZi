using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.UI.Animators
{
    // todo rework to UiAnimationFromValue
    public class UiCanvasGroupAlphaAnimation : MonoBehaviourExtContainer, IUiElementAnimation
    {
        private const float _transparentAlpha = 0f;
        private const float _displayedAlpha = 1f;
        private readonly UpdateActionType.Mode _updatingMode;
        private readonly CanvasGroup _canvasGroup;
        private readonly AnimationCurve _curve;
        private readonly float _animationTime;
        private ICoroutine _animation;

        public UiCanvasGroupAlphaAnimation(MonoBehaviourExt mono,
                                  CanvasGroup canvasGroup,
                                  UpdateActionType.Mode updatingMode,
                                  AnimationCurve curve,
                                  float animationTime) : base(mono)
        {
            _canvasGroup = canvasGroup != null
                ? canvasGroup
                : throw new ArgumentNullException(nameof(canvasGroup));

            _updatingMode = updatingMode;
            _animationTime = animationTime;
            _curve = curve ?? throw new ArgumentNullException(nameof(curve));
            _animation = new CoroutineWrap(mono);
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
            float counter = 0;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => Alpha > _transparentAlpha, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                counter -= delta;
                Alpha = _curve.Evaluate(counter);
            });

            SetDisplayed();
            counter = Alpha;
            yield return _animation.StartNested(enumerator);
            SetHidden();
            OnEnded?.Invoke();
        }

        private IEnumerator ToDisplayed(Action OnEnded)
        {
            float counter = 0;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => Alpha < _displayedAlpha, (deltaTime) =>
            {

                float delta = 1f / _animationTime * deltaTime;
                counter += delta;
                Alpha = _curve.Evaluate(counter);
            });

            SetHidden();
            counter = Alpha;
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
