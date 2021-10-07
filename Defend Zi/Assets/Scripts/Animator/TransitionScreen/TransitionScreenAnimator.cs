using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TransitionScreenAnimator : MonoBehaviourExt
{
    private readonly float animationTime = 0.25f;
    private Image _image;

    private ICoroutine _animation;

    protected override void AwakeExt()
    {
        _image = GetComponent<Image>();
        _animation = new CoroutineWrap(this);
    }

    public event Action OnDisplayed;
    public event Action OnHidden;

    private Color Color { get => _image.color; set => _image.color = value; }

    public void Show()
    {
        SetHidden();
        _animation.ReStart(ToDisplayed());
    }

    public void Hide()
    {
        SetDisplayed();
        _animation.ReStart(ToHidden());
    }

    private IEnumerator ToHidden()
    {
        while (Color.a > 0)
        {
            float delta = 1f / animationTime * Time.unscaledDeltaTime;
            SetColorAlpha(Color.a - delta);
            yield return null;
        }
        SetHidden();
        OnHidden?.Invoke();
    }

    private IEnumerator ToDisplayed()
    {
        SetRaycastTarget(true);
        while (Color.a < 1)
        {
            float delta = 1f / animationTime * Time.unscaledDeltaTime;
            SetColorAlpha(Color.a + delta);
            yield return null;
        }
        SetDisplayed();
        OnDisplayed?.Invoke();
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
