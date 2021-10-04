using Desdiene.DataStorageFactories.Validators;

namespace Desdiene.DataStorageFactories.Datas
{
    public interface IData : IDataAccessor, IDataMutator, IDataNotifier, IDataValidator
    {

    }
}
