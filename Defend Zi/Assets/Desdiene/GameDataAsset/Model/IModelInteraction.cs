using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.Model
{
    public interface IModelInteraction<T> where T : IData
    {
        T GetData();
        void SetData(T data);
    }
}
