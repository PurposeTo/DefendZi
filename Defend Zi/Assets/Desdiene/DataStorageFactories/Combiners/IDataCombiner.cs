using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.Combiners
{
    public interface IDataCombiner<T> where T : IData
    {
        T Combine(T first, T second);
    }
}
