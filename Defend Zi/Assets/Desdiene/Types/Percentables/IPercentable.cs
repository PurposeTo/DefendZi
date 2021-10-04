using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Percents;

namespace Desdiene.Types.Percentale
{
    public interface IPercentable<T> : IRef<T>, IPercent, IPercentableAccessor<T>, IPercentableMutator<T>, IPercentableNotifier
    {

    }
}
