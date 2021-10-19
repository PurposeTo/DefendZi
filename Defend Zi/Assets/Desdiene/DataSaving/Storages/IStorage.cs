namespace Desdiene.DataSaving.Storages
{
    public interface IStorage<T>
    {
        string StorageName { get; }

        /// <returns>успешно?</returns>
        bool TryLoad(out T data);


        /// <returns>успешно?</returns>
        bool Save(T data);

        /// <returns>успешно?</returns>
        bool TryToClean();
    }
}
