using System;

namespace Desdiene.Types.ValuesInRange
{
    public interface IReadPercentable
    {
        event Action OnValueChanged;
        float GetPercent();
    }
}
