using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
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
            IPercent canvasGroupAlpha = new CanvasGroupAsPercentAlpha(canvasGroup);
            _uiElementAnimation = new UiAnimationFromValue(this, _updatingMode, _curve, _animatingTime, canvasGroupAlpha);
        }

        void IUiElementAnimation.Hide(Action OnEnded) => _uiElementAnimation.Hide(OnEnded);

        void IUiElementAnimation.Show(Action OnEnded) => _uiElementAnimation.Show(OnEnded);
    }
}
