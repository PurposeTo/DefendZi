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

    public event Action OnMakingVisible;
    public event Action OnMakedTransparent;

    protected override void AwakeAnimator()
    {
        _isTransparent = GetAnimatorBool(isTransparentField, true);
    }

    public void MakeVisible() => _isTransparent.Value = false;
    public void MakeTransparent() => _isTransparent.Value = true;

    // вызывается анимацией
    private void InvokeOnMakingVisible() => OnMakingVisible?.Invoke();

    // вызывается анимацией
    private void InvokeOnMakedTransparent() => OnMakedTransparent?.Invoke();
}
