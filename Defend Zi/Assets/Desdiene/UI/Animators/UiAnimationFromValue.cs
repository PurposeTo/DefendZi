using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using UnityEngine;

namespace Desdiene.UI.Animators
{
    /// <summary>
    /// Анимирует показ/скрытие UI элемента путем изменения IPercent значения.
    /// IPercent _animated.Min = hidden
    /// IPercent _animated.Max = displayed
    /// </summary>
    public class UiAnimationFromValue : MonoBehaviourExtContainer, IUiElementAnimation
    {
        private readonly UpdateActionType.Mode _updatingMode;
        private readonly AnimationCurve _curve;
        private readonly float _animationTime;
        private readonly ICoroutine _animation;
        private readonly IPercent _animatedValue;

        public UiAnimationFromValue(MonoBehaviourExt mono,
                                    UpdateActionType.Mode updatingMode,
                                    AnimationCurve curve,
                                    float animationTime,
                                    IPercent animated) : base(mono)
        {
            _updatingMode = updatingMode;
            _animationTime = animationTime;
            _curve = curve ?? throw new ArgumentNullException(nameof(curve));
            _animation = new CoroutineWrap(mono);
            _animatedValue = animated ?? throw new ArgumentNullException(nameof(animated));
        }

        void IUiElementAnimation.Show(Action OnEnded)
        {
            _animation.ReStart(ToDisplayed(OnEnded));
        }

        void IUiElementAnimation.Hide(Action OnEnded)
        {
            _animation.ReStart(ToHidden(OnEnded));
        }

        private IEnumerator ToHidden(Action OnEnded)
        {
            _animatedValue.SetMax();
            float counter = _animatedValue.Value;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => !_animatedValue.IsMin, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                counter -= delta;
                _animatedValue.Set(_curve.Evaluate(counter));
            });

            yield return _animation.StartNested(enumerator);
            _animatedValue.SetMin();
            OnEnded?.Invoke();
        }

        private IEnumerator ToDisplayed(Action OnEnded)
        {
            _animatedValue.SetMin();
            float counter = _animatedValue.Value;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => !_animatedValue.IsMax, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                counter += delta;
                _animatedValue.Set(_curve.Evaluate(counter));
            });

            yield return _animation.StartNested(enumerator);
            _animatedValue.SetMax();
            OnEnded?.Invoke();
        }
    }
}
