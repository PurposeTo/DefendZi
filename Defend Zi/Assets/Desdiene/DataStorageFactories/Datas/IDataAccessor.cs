using System;

namespace Desdiene.DataStorageFactories.Datas
{
    public interface IDataAccessor
    {
        //Методы или свойства по получению данных
        TimeSpan PlayingTime { get; }
    }
}
