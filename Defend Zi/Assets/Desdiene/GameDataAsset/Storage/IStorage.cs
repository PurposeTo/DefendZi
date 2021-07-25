using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.Storage
{
    public interface IStorage<T> where T : IData
    {
        T GetData(); 

        void LoadFromStorage();

        void SaveToStorage();
    }
}
