using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.Model
{
    public class DataModel<TData> :
        IData,
        IModelInteraction<TData>
        where TData : IData, new()
    {
        public DataModel()
        {
            data = new TData();
        }

        private TData data; // Не может быть null

        TData IModelInteraction<TData>.GetData() => data;

        void IModelInteraction<TData>.SetData(TData data) => this.data = data;
    }
}
