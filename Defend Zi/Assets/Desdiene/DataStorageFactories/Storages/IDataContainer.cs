using Desdiene.DataStorageFactories.Datas;

namespace Desdiene.DataStorageFactories.Storages
{
    public interface IDataContainer<T> where T : IData
    {
        T GetData();

        void InvokeLoadingData();

        void InvokeSavingData();
    }
}
