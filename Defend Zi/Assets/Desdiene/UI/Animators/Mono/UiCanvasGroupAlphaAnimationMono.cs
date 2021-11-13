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

        protected override void AwakeExt()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            _uiElementAnimation = new UiCanvasGroupAlpha(this, canvasGroup, _updatingMode, _animatingTime);
        }

        void IUiElementAnimation.Hide(Action OnEnded) => _uiElementAnimation.Hide(OnEnded);

        void IUiElementAnimation.Show(Action OnEnded) => _uiElementAnimation.Show(OnEnded);
    }
}
