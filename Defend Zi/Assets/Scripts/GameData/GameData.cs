using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.Datas;
using UnityEngine;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class GameData : IGameData, IDataCombiner<GameData>
{
    public TimeSpan PlayingTime { get; set; } = TimeSpan.Zero;

    public uint GamesNumber { get; set; } = 0;

    public uint BestScore { get; set; } = 0;

    public TimeSpan AverageLifeTime => GamesNumber == 0
        ? TimeSpan.Zero
        : TimeSpan.FromSeconds(PlayingTime.Seconds / GamesNumber);

    public TimeSpan BestLifeTime { get; set; } = TimeSpan.Zero;

    bool IValidData.IsValid() => IsValid();

    void IValidData.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    void IDataMutator.SetPlayingTime(TimeSpan time) => PlayingTime = time;

    void IDataMutator.AddPlayingTime(TimeSpan time) => PlayingTime += time;

    void IGameData.IncreaseGamesNumber() => GamesNumber++;

    void IGameData.SetBestScore(uint score)
    {
        BestScore = Desdiene.Math.ClampMin(score, BestScore);
    }

    void IGameData.SetBestLifeTime(TimeSpan time)
    {
        BestLifeTime = new List<TimeSpan>() { time, BestLifeTime }.Max();
    }

    GameData IDataCombiner<GameData>.Combine(GameData first, GameData second)
    {
        GameData gameData = new GameData();

        // combine PlayingTime
        gameData.PlayingTime = first.PlayingTime + second.PlayingTime;

        //combine GamesNumber
        uint fullGamesNumber = first.GamesNumber + second.GamesNumber;
        gameData.GamesNumber = fullGamesNumber;

        //combine BestScore
        gameData.BestScore = (uint)Mathf.Max(first.BestScore, second.BestScore);

        //combine BestLifeTime
        gameData.BestLifeTime = new List<TimeSpan>() { first.BestLifeTime, second.BestLifeTime }.Max();

        return gameData;
    }

    public override string ToString()
    {
        return $"{GetType().Name}"
             + $"\nGamesNumber={GamesNumber}"
             + $"\nBestScore={BestScore}"
             + $"\nAverageLifeTimeSec={AverageLifeTime}"
             + $"\nBestLifeTimeSec={BestLifeTime}"
             + $"\nPlayingTime={PlayingTime}";
    }

    public override bool Equals(object obj)
    {
        return obj is GameData data &&
               PlayingTime.Equals(data.PlayingTime) &&
               GamesNumber == data.GamesNumber &&
               BestScore == data.BestScore &&
               AverageLifeTime.Equals(data.AverageLifeTime) &&
               BestLifeTime.Equals(data.BestLifeTime);
    }

    public override int GetHashCode()
    {
        int hashCode = -338788781;
        hashCode = hashCode * -1521134295 + PlayingTime.GetHashCode();
        hashCode = hashCode * -1521134295 + GamesNumber.GetHashCode();
        hashCode = hashCode * -1521134295 + BestScore.GetHashCode();
        hashCode = hashCode * -1521134295 + AverageLifeTime.GetHashCode();
        hashCode = hashCode * -1521134295 + BestLifeTime.GetHashCode();
        return hashCode;
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
}
