using System;

namespace Desdiene.UI.Animators
{
    public class UiElementAnimationEmpty : IUiElementAnimation
    {
        void IUiElementAnimation.Show(Action OnEnded) => OnEnded?.Invoke();
        void IUiElementAnimation.Hide(Action OnEnded) => OnEnded?.Invoke();
    }
}
