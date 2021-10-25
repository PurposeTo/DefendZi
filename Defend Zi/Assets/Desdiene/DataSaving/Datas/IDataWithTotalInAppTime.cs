using System;

namespace Desdiene.DataSaving.Datas
{
    public interface IDataWithTotalInAppTime
    {
        TimeSpan TotalInAppTime { get; }
    }
}
