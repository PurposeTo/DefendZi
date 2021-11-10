using System.Collections.Generic;
using Desdiene.Types.Percents;

namespace Desdiene.Randoms
{
    public interface ISelectableItems<T> : IEnumerable<T>
    {
        T GetRandom();
        IPercentAccessor GetChance(ISelectableItem<T> item);
    }
}
