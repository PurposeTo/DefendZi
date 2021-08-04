using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.Combiner
{
    public interface IDataCombiner<T> where T : IData
    {
        T Combine(T first, T second);
    }
}
