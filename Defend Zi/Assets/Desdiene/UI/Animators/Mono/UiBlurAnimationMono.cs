using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

namespace Desdiene.UI.Animators
{
    [RequireComponent(typeof(Image))]
    public class UiBlurAnimationMono : MonoBehaviourExt, IUiElementAnimation
    {
        [SerializeField, NotNull] private Shader _blurShader;
        [SerializeField] private UpdateActionType.Mode _updatingMode;
        [SerializeField] private float _animatingTime = 0.15f;
        private IUiElementAnimation _uiElementAnimation;
        private Image _image;
        private AnimationCurve _curve;

        protected override void AwakeExt()
        {
            _image = GetComponent<Image>();

            Color color = _image.color;
            float alphaColor = color.a;

            // fixme не работает!
            color = new Color(color.r * alphaColor,
                              color.g * alphaColor,
                              color.b * alphaColor,
                              1);
            //Blur _blur = new Blur(_blurShader, color);

            Blur _blur = new Blur(_blurShader);
            _image.material = _blur.Material;
            _curve = AnimationCurveFactory.Get(AnimationCurveFactory.CurveType.Linear);
            _uiElementAnimation = new UiAnimationFromValue(this, _updatingMode, _curve, _animatingTime, _blur);
        }

        void IUiElementAnimation.Hide(Action OnEnded) => _uiElementAnimation.Hide(OnEnded);

        void IUiElementAnimation.Show(Action OnEnded)
        {
            _uiElementAnimation.Show(OnEnded);
        }
    }
}
