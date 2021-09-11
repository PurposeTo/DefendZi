using System;
using Desdiene.AnimatorExtension;
using UnityEngine;

/// <summary>
/// Класс описывает анимацию перехода прозрачного элемента в не прозрачный и обратно.
/// Использовать класс как модель.
/// Можно навесить на объект со sprite/text/image и настроить анимации с gameObject-ом.
/// По умолчанию спрайт прозрачный.
/// По включению плавно уменьшает прозрачность.
/// По выключению плавно увеличивает прозрачность.
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class ToVisible : AnimatorModel
{
    private readonly string isTransparentField = "isTransparent";
    private AnimatorBool _isTransparent;

    public event Action OnAnimationEnabling;
    public event Action OnAnimationDisabled;

    protected override void AwakeAnimator()
    {
        _isTransparent = GetAnimatorBool(isTransparentField, true);
    }

    public void Enable() => _isTransparent.Value = false;
    public void Disable() => _isTransparent.Value = true;

    // вызывается анимацией
    private void InvokeOnAnimationEnabling() => OnAnimationEnabling?.Invoke();

    // вызывается анимацией
    private void InvokeOnAnimationDisabled() => OnAnimationDisabled?.Invoke();
}
