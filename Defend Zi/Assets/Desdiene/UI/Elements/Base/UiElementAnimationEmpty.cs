using System;

namespace Desdiene.UI.Elements
{
    public class UiElementAnimationEmpty : IUiElementAnimation
    {
        void IUiElementAnimation.Show(Action OnEnded) => OnEnded?.Invoke();
        void IUiElementAnimation.Hide(Action OnEnded) => OnEnded?.Invoke();
    }
}
