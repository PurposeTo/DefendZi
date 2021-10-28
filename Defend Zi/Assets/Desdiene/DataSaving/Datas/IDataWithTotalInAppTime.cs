using System;

namespace Desdiene.DataSaving.Datas
{
    /// <summary>
    /// Необходим для разрешения конфликтов при взаимодействии с хранилищем посредством выбора данных с самым большим значением TotalInAppTime
    /// </summary>
    public interface IDataWithTotalInAppTime
    {
        TimeSpan TotalInAppTime { get; }
    }
}
