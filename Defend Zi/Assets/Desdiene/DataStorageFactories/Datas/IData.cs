using Desdiene.DataStorageFactories.Validators;

namespace Desdiene.DataStorageFactories.Datas
{
    public interface IData : IDataGetter, IDataSetter, IDataNotifier, IDataValidator
    {

    }
}
