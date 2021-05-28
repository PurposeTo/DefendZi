using System;

namespace Desdiene.Types.ValuesInRange.Interfaces
{
    /// <summary>
    /// Сущность, у которой можно взять процентное значение.
    /// </summary>
    public interface IReadPercentable
    {
        event Action OnValueChanged;
        float GetPercent();
    }
}
