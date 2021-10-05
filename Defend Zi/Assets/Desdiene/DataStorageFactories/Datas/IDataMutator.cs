using System;

namespace Desdiene.DataStorageFactories.Datas
{
    public interface IDataMutator
    {
        //Методы или свойства по установке данных

        void SetPlayingTime(TimeSpan time);
        void AddPlayindTime(TimeSpan time);
    }
}
