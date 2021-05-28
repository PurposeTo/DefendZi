
namespace Desdiene.Types.AtomicReference.Interfaces
{
    public interface IRef<T> : IReadRef<T>, IWriteRef<T>
    {
        public T SetAndGet(T value);
        //Не поддерживается в данной версии :(
        //{
        //    Set(value);
        //    return Get();
        //}
    }
}
