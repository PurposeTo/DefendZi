using Desdiene.DataStorageFactories.Data;

namespace Desdiene.DataStorageFactories.Combiner
{
    public interface IDataCombiner<T> where T : IData
    {
        T Combine(T first, T second);
    }
}
