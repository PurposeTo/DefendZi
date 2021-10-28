using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class GameStatisticsDto : IValidData, IDataWithTotalInAppTime, IJsonSerializable
{
    bool IValidData.IsValid() => IsValid();

    void IValidData.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    string IJsonSerializable.ToJson()
    {
        IJsonSerializer<GameStatisticsDto> jsonSerializer = new GameStatisticsDtoJsonConvertor();
        return jsonSerializer.ToJson(this);
    }

    public TimeSpan TotalInAppTime { get; set; } = TimeSpan.Zero;
    public TimeSpan TotalLifeTime { get; set; } = TimeSpan.Zero;
    public uint GamesNumber { get; set; } = 0;
    public TimeSpan BestLifeTime { get; set; } = TimeSpan.Zero;
    public uint BestScore { get; set; } = 0;

    public override string ToString()
    {
        return $"{GetType().Name}"
            + $"\nTotalInAppTime={TotalInAppTime}"
            + $"\nTotalLifeTime={TotalLifeTime}"
            + $"\nGamesNumber={GamesNumber}"
            + $"\nBestLifeTimeSec={BestLifeTime}"
            + $"\nBestScore={BestScore}";
    }

    private bool IsValid()
    {
        // сейчас нельзя сломать данные, т.к. нет nullable полей.
        return true;
    }

    private void Repair()
    {
        // сейчас нельзя сломать данные, т.к. нет nullable полей.
    }

    public override bool Equals(object obj)
    {
        return obj is GameStatisticsDto data &&
               TotalInAppTime.Equals(data.TotalInAppTime) &&
               TotalLifeTime.Equals(data.TotalLifeTime) &&
               GamesNumber == data.GamesNumber &&
               BestScore == data.BestScore &&
               BestLifeTime.Equals(data.BestLifeTime);
    }

    public override int GetHashCode()
    {
        int hashCode = -648678592;
        hashCode = hashCode * -1521134295 + TotalInAppTime.GetHashCode();
        hashCode = hashCode * -1521134295 + TotalLifeTime.GetHashCode();
        hashCode = hashCode * -1521134295 + GamesNumber.GetHashCode();
        hashCode = hashCode * -1521134295 + BestLifeTime.GetHashCode();
        hashCode = hashCode * -1521134295 + BestScore.GetHashCode();
        return hashCode;
    }
}
