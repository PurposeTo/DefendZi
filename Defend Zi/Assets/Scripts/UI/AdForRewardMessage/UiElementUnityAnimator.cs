using System;
using Desdiene.AnimatorExtension;
using Desdiene.UI.Elements;
using UnityEngine;

public class UiElementUnityAnimator : AnimatorModel, IUiElementAnimation
{
    [SerializeField] private bool _isHiddenOnInited;
    private AnimatorBool IsHidden;

    protected override void AwakeAnimator()
    {
        IsHidden = GetAnimatorBool("isHidden", _isHiddenOnInited);
    }

    private event Action OnHidden;
    private event Action OnDisplayed;

    void IUiElementAnimation.Show(Action OnEnded)
    {
        if (!IsHidden.Value)
        {
            OnEnded?.Invoke();
            return;
        }

        IsHidden.Value = false;

        void InvokeOnEnded()
        {
            OnEnded?.Invoke();
            OnDisplayed -= InvokeOnEnded;
        }
        OnDisplayed += InvokeOnEnded;
    }

    void IUiElementAnimation.Hide(Action OnEnded)
    {
        if (IsHidden.Value)
        {
            OnEnded?.Invoke();
            return;
        }

        IsHidden.Value = true;

        void InvokeOnEnded()
        {
            OnEnded?.Invoke();
            OnHidden -= InvokeOnEnded;
        }
        OnHidden += InvokeOnEnded;
    }

    // Вызывается аниматором
    private void InvokeOnHidden() => OnHidden?.Invoke();

    // Вызывается аниматором
    private void InvokeOnDisplayed() => OnDisplayed?.Invoke();
}
