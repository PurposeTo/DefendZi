using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UI.Elements;


namespace Desdiene.UI.Animators
{
    public class UiElementUnityEmptyAnimator : MonoBehaviourExt, IUiElementAnimation
    {
        void IUiElementAnimation.Hide(Action OnEnded)
        {
            OnEnded?.Invoke();
        }

        void IUiElementAnimation.Show(Action OnEnded)
        {
            OnEnded?.Invoke();
        }
    }
}
