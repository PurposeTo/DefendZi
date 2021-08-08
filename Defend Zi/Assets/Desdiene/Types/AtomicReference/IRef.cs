
namespace Desdiene.Types.AtomicReference
{
    public interface IRef<T> : IRefGetter<T>, IRefSetter<T>, IRefNotifier
    {
        public T SetAndGet(T value);
        //Не поддерживается в данной версии :(
        //{
        //    Set(value);
        //    return Get();
        //}
    }
}
