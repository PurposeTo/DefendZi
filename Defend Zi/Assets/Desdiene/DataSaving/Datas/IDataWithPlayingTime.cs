using System;

namespace Desdiene.DataSaving.Datas
{
    public interface IDataWithPlayingTime
    {
        TimeSpan PlayingTime { get; }
    }
}
