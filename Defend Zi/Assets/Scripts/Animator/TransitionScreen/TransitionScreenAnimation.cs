using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UI.Animators;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TransitionScreenAnimation : MonoBehaviourExt, IUiElementAnimation
{
    private readonly float animationTime = 0.15f;
    private Image _image;

    private ICoroutine _animation;

    protected override void AwakeExt()
    {
        _image = GetComponent<Image>();
        _animation = new CoroutineWrap(this);
    }

    private Color Color { get => _image.color; set => _image.color = value; }

    void IUiElementAnimation.Show(Action OnEnded)
    {
        SetHidden();
        _animation.ReStart(ToDisplayed(OnEnded));
    }

    void IUiElementAnimation.Hide(Action OnEnded)
    {
        SetDisplayed();
        _animation.ReStart(ToHidden(OnEnded));
    }

    private IEnumerator ToHidden(Action OnEnded)
    {
        while (Color.a > 0)
        {
            float delta = 1f / animationTime * Time.unscaledDeltaTime;
            SetColorAlpha(Color.a - delta);
            yield return null;
        }
        SetHidden();
        OnEnded?.Invoke();
    }

    private IEnumerator ToDisplayed(Action OnEnded)
    {
        SetRaycastTarget(true);
        while (Color.a < 1)
        {
            float delta = 1f / animationTime * Time.unscaledDeltaTime;
            SetColorAlpha(Color.a + delta);
            yield return null;
        }
        SetDisplayed();
        OnEnded?.Invoke();
    }

    private void SetDisplayed()
    {
        SetRaycastTarget(true);
        SetColorAlpha(1);
    }

    private void SetHidden()
    {
        SetRaycastTarget(false);
        SetColorAlpha(0);
    }

    private void SetColorAlpha(float alpha)
    {
        Color currentColor = _image.color;
        _image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }

    private void SetRaycastTarget(bool isTarget)
    {
        _image.raycastTarget = isTarget;
    }
}
