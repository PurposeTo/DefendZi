using System;

namespace Desdiene.UI.Animators
{
    /// <summary>
    /// интерфейс описывает UI элемент, находящийся на Canvas overlay
    /// </summary>
    public interface IUiElementAnimation
    {
        void Show(Action OnEnded);
        void Hide(Action OnEnded);
    }
}
