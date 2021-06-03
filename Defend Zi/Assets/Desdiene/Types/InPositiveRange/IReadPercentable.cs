using System;

namespace Desdiene.Types.InPositiveRange
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
