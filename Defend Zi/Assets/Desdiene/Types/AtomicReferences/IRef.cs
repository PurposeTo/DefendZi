
namespace Desdiene.Types.AtomicReferences
{
    public interface IRef<T> : IRefAccessor<T>, IRefMutator<T>, IRefNotifier
    {
        public T SetAndGet(T value);
        //Не поддерживается в данной версии :(
        //{
        //    Set(value);
        //    return Get();
        //}
    }
}
