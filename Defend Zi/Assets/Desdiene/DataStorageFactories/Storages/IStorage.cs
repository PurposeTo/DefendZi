using Desdiene.DataStorageFactories.Data;

namespace Desdiene.DataStorageFactories.Storages
{
    public interface IStorage<T> where T : IData
    {
        T GetData();

        void InvokeLoadingData();

        void InvokeSavingData();
    }
}
