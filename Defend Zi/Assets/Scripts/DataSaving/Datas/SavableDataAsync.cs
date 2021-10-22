using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class SavableDataAsync : IValidData, IDataWithPlayingTime, IJsonSerializable
{
    bool IValidData.IsValid() => IsValid();

    void IValidData.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    string IJsonSerializable.ToJson()
    {
        IJsonSerializer<SavableDataAsync> jsonSerializer = new DataAsyncJsonConvertor();
        return jsonSerializer.ToJson(this);
    }

    public TimeSpan PlayingTime { get; set; } = TimeSpan.Zero;

    public uint GamesNumber { get; set; } = 0;

    public uint BestScore { get; set; } = 0;

    public TimeSpan BestLifeTime { get; set; } = TimeSpan.Zero;

    public override string ToString()
    {
        return $"{GetType().Name}"
             + $"\nGamesNumber={GamesNumber}"
             + $"\nBestScore={BestScore}"
             + $"\nBestLifeTimeSec={BestLifeTime}"
             + $"\nPlayingTime={PlayingTime}";
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
        return obj is SavableDataAsync data &&
               PlayingTime.Equals(data.PlayingTime) &&
               GamesNumber == data.GamesNumber &&
               BestScore == data.BestScore &&
               BestLifeTime.Equals(data.BestLifeTime);
    }

    public override int GetHashCode()
    {
        int hashCode = -648678592;
        hashCode = hashCode * -1521134295 + PlayingTime.GetHashCode();
        hashCode = hashCode * -1521134295 + GamesNumber.GetHashCode();
        hashCode = hashCode * -1521134295 + BestScore.GetHashCode();
        hashCode = hashCode * -1521134295 + BestLifeTime.GetHashCode();
        return hashCode;
    }
}
