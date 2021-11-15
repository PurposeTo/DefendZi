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
        private ICoroutine _animation;
        private IPercent _animated;

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
            _animated = animated ?? throw new ArgumentNullException(nameof(animated));
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
            _animated.SetMax();
            float counter = _animated.Value;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => !_animated.IsMin, (deltaTime) =>
            {
                float delta = 1f / _animationTime * deltaTime;
                counter -= delta;
                _animated.Set(_curve.Evaluate(counter));
            });

            yield return _animation.StartNested(enumerator);
            _animated.SetMin();
            OnEnded?.Invoke();
        }

        private IEnumerator ToDisplayed(Action OnEnded)
        {
            _animated.SetMin();
            float counter = _animated.Value;

            IEnumerator enumerator = UpdateActionType.GetIEnumerator(_updatingMode, () => !_animated.IsMax, (deltaTime) =>
            {

                float delta = 1f / _animationTime * deltaTime;
                counter += delta;
                _animated.Set(_curve.Evaluate(counter));
            });

            yield return _animation.StartNested(enumerator);
            _animated.SetMax();
            OnEnded?.Invoke();
        }
    }
}
