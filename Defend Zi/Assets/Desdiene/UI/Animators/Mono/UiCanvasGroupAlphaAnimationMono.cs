using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.UI.Animators
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UiCanvasGroupAlphaAnimationMono : MonoBehaviourExt, IUiElementAnimation
    {
        [SerializeField] private UpdateActionType.Mode _updatingMode;
        [SerializeField] private float _animatingTime = 0.15f;
        private IUiElementAnimation _uiElementAnimation;
        private AnimationCurve _curve;

        protected override void AwakeExt()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            _curve = AnimationCurveFactory.Get(AnimationCurveFactory.CurveType.Linear);
            _uiElementAnimation = new UiCanvasGroupAlphaAnimation(this, canvasGroup, _updatingMode, _curve, _animatingTime);
        }

        void IUiElementAnimation.Hide(Action OnEnded) => _uiElementAnimation.Hide(OnEnded);

        void IUiElementAnimation.Show(Action OnEnded) => _uiElementAnimation.Show(OnEnded);
    }
}
