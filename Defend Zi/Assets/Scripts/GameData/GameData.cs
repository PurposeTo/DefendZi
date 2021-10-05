using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.Validators;
using UnityEngine;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class GameData : IGameData, IDataCombiner<GameData>
{

    public TimeSpan PlayingTime { get; set; } = TimeSpan.Zero;

    public int GamesNumber { get; set; } = 0;

    public int BestScore { get; set; } = 0;

    public TimeSpan AverageLifeTime => GamesNumber == 0
        ? TimeSpan.Zero
        : TimeSpan.FromSeconds(PlayingTime.Seconds / GamesNumber);

    public TimeSpan BestLifeTime { get; set; } = TimeSpan.Zero;

    bool IDataValidator.IsValid() => IsValid();

    void IDataValidator.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    void IDataMutator.SetPlayingTime(TimeSpan time) => PlayingTime = time;

    void IDataMutator.AddPlayingTime(TimeSpan time) => PlayingTime += time;

    void IGameData.IncreaseGamesNumber() => GamesNumber++;

    void IGameData.SetBestScore(uint score)
    {
        BestScore = Desdiene.Math.ClampMin((int)score, BestScore);
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
        int fullGamesNumber = first.GamesNumber + second.GamesNumber;
        gameData.GamesNumber = fullGamesNumber;

        //combine BestScore
        gameData.BestScore = Mathf.Max(first.BestScore, second.BestScore);

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
             + $"\nBestLifeTimeSec={BestLifeTime}";
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
