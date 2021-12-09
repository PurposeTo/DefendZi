using System;

namespace Desdiene.EditorTools
{
    /// <summary>
    /// Получение значения состояния в виде строки для выведения в unity inspector
    /// </summary>
   public interface IDebugState
    {
        event Action<string> WhenChangedName;
    }
}
