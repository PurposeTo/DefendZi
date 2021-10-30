using Desdiene.Types.Percents;
using System;
using System.Collections.Generic;

namespace Desdiene.Randoms
{
    public interface ISelectableItems<T> : IEnumerable<T>
    {
        T GetRandom();
        IPercentAccessor GetChance(ISelectableItem<T> item);
    }
}
