using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// интерфейс описывает UI элемент, находящийся на Canvas overlay
    /// </summary>
    public interface IUiElement
    {
        IProcessAccessorNotifier Show();
        IProcessAccessorNotifier Hide();
    }
}
