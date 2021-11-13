using System;
using Desdiene.MonoBehaviourExtension;


namespace Desdiene.UI.Animators
{
    public class UiElementAnimationEmptyMono : MonoBehaviourExt, IUiElementAnimation
    {
        void IUiElementAnimation.Show(Action OnEnded) => OnEnded?.Invoke();
        void IUiElementAnimation.Hide(Action OnEnded) => OnEnded?.Invoke();
    }
}
