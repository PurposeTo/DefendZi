using System;
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
    TimeSpan IDataAccessor.PlayingTime => PlayingTime;

    GameData IDataCombiner<GameData>.Combine(GameData first, GameData second)
    {
        GameData gameData = new GameData();

        //combine GamesNumber
        int fullGamesNumber = first.GamesNumber + second.GamesNumber;
        gameData.GamesNumber = fullGamesNumber;

        //combine BestScore
        gameData.BestScore = Mathf.Max(first.BestScore, second.BestScore);

        //combine AverageLifeTimeSec
        if (fullGamesNumber != 0)
        {
            int fullLifeTime = first.AverageLifeTimeSec * first.GamesNumber + second.AverageLifeTimeSec * second.GamesNumber;
            gameData.AverageLifeTimeSec = fullLifeTime / fullGamesNumber;
        }

        return gameData;
    }

    bool IDataValidator.IsValid() => IsValid();

    void IDataValidator.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    void IDataMutator.SetPlayingTime(TimeSpan time) => PlayingTime = time;

    void IDataMutator.AddPlayindTime(TimeSpan time) => PlayingTime += time;

    public TimeSpan PlayingTime { get; set; } = TimeSpan.Zero;

    public int GamesNumber { get; set; } = 0;

    public int BestScore { get; set; } = 0;

    public int AverageLifeTimeSec { get; set; } = 0;
    public int BestLifeTimeSec { get; set; } = 0;


    public void IncreaseGamesNumber() => GamesNumber++;

    public void SetBestScore(uint score)
    {
        BestScore = Desdiene.Math.ClampMin((int)score, BestScore);
    }

    /// <summary>
    /// Данный метод 
    /// </summary>
    /// <param name="timeSec"></param>
    public void SetAverageLifeTimeSec(uint timeSec)
    {
        int previosLifeTime = AverageLifeTimeSec * (GamesNumber - 1);
        int fullLifeTime = (int)(previosLifeTime + timeSec);

        if (GamesNumber != 0) AverageLifeTimeSec = fullLifeTime / GamesNumber;
    }

    public void SetBestLifeTimeSec(uint timeSec)
    {
        BestLifeTimeSec = Desdiene.Math.ClampMin((int)timeSec, BestLifeTimeSec);
    }

    public override string ToString()
    {
        return $"{GetType().Name}"
             + $"\nGamesNumber={GamesNumber}"
             + $"\nBestScore={BestScore}"
             + $"\nAverageLifeTimeSec={AverageLifeTimeSec}"
             + $"\nBestLifeTimeSec={BestLifeTimeSec}";
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
