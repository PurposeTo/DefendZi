using UnityEngine;

namespace Desdiene.UI.Elements
{
    [RequireComponent(typeof(IUiElementAnimation))]
    public class PopUpWindowWithUnityAnimation : PopUpWindow
    {
        private IUiElementAnimation _animation;
        protected override IUiElementAnimation Animation => _animation;
        protected override void AwakeWindow()
        {
            _animation = GetInitedComponent<IUiElementAnimation>();
        }
    }
}
