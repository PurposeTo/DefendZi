using System;
using Desdiene.AnimatorExtension;
using UnityEngine;

/// <summary>
/// Класс описывает анимацию перехода прозрачного элемента в мигание и обратно.
/// Использовать класс как модель.
/// Можно навесить на объект со sprite/text/image и настроить анимации с gameObject-ом.
/// 
/// По умолчанию спрайт прозрачный.
/// По включению плавно уменьшает прозрачность.
/// Когда становиться полностью не прозрачным, начинает мигать. Циклично.
/// По выключению плавно увеличивает прозрачность.
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class FromTransparentToBlinking : AnimatorModel
{
    private readonly string isBlinkingField = "isBlinking";
    private AnimatorBool _isBlinking;

    public event Action OnAnimationEnabling;
    public event Action OnAnimationDisabled;

    protected override void AwakeAnimator()
    {
        _isBlinking = GetAnimatorBool(isBlinkingField, false);
    }

    public void Enable() => _isBlinking.Value = true;
    public void Disable() => _isBlinking.Value = false;

    // вызывается анимацией
    private void InvokeOnAnimationEnabling() => OnAnimationEnabling?.Invoke();

    // вызывается анимацией
    private void InvokeOnAnimationDisabled() => OnAnimationDisabled?.Invoke();
}
