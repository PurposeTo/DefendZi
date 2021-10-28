using System;
using Desdiene.DataSaving.Datas;
using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

/// <summary>
/// Статистика игры.
/// Класс сделан MonoBehaviour для возможности чтения полей через инспектор.
/// </summary>
public class GameStatistics : MonoBehaviourExt, IGameStatistics
{
    private IStorageAsync<GameStatisticsDto> _storage;
    // todo добавить события об изменении
    private TimeSpan _totalInAppTime = TimeSpan.Zero;
    private TimeSpan _totalLifeTime = TimeSpan.Zero;
    private uint _gamesNumber = 0;
    private uint _bestScore = 0;
    private TimeSpan _bestLifeTime = TimeSpan.Zero;

    [Inject]
    private void Constructor(IStorageAsync<GameStatisticsDto> storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    protected override void AwakeExt()
    {
        SubscribeEvents();
        _storage.Read();
    }

    protected override void OnDestroyExt()
    {
        Save();
        UnsubscribeEvents();
    }

    private void Update()
    {
        float unscaledDeltaTime = Time.unscaledDeltaTime;
        TimeSpan time = TimeSpan.FromSeconds(unscaledDeltaTime);
        AddTotalInAppTime(time);
    }

    TimeSpan IGameStatisticsAccessor.TotalInAppTime => _totalInAppTime;
    TimeSpan IGameStatisticsAccessor.TotalLifeTime => _totalLifeTime;
    uint IGameStatisticsAccessor.GamesNumber => _gamesNumber;
    uint IGameStatisticsAccessor.BestScore => _bestScore;
    TimeSpan IGameStatisticsAccessor.BestLifeTime => _bestLifeTime;
    void ISavableData.Save() => Save();
    void IGameStatisticsMutator.AddLifeTime(TimeSpan value) => AddLifeTime(value);
    void IGameStatisticsMutator.IncrementGamesNumber() => IncrementGamesNumber();
    void IGameStatisticsMutator.SetBestLifeTime(TimeSpan value) => SetBestLifeTime(value);
    void IGameStatisticsMutator.SetBestScore(uint value) => SetBestScore(value);

    private void Save()
    {
        var dto = new GameStatisticsDto()
        {
            TotalInAppTime = _totalInAppTime,
            TotalLifeTime = _totalLifeTime,
            GamesNumber = _gamesNumber,
            BestLifeTime = _bestLifeTime,
            BestScore = _bestScore,
        };

        _storage.Update(dto);
    }

    private void AddTotalInAppTime(TimeSpan value) => SetTotalInAppTime(_totalInAppTime + value);

    private void SetTotalInAppTime(TimeSpan value)
    {
        _totalInAppTime = value;
    }

    private void AddLifeTime(TimeSpan value) => SetTotalLifeTime(_totalLifeTime + value);

    private void SetTotalLifeTime(TimeSpan value)
    {
        _totalLifeTime = value;
    }

    private void IncrementGamesNumber() => SetGamesNumber(_gamesNumber + 1);

    private void SetGamesNumber(uint value)
    {
        _gamesNumber = value;
    }

    private void SetBestLifeTime(TimeSpan value)
    {
        if (value <= _bestLifeTime) return;

        _bestLifeTime = value;
    }

    private void SetBestScore(uint value)
    {
        if (value <= _bestScore) return;

        _bestScore = value;
    }

    private void SubscribeEvents()
    {
        _storage.OnReaded += UpdateDataFromDto;
    }

    private void UnsubscribeEvents()
    {
        _storage.OnReaded -= UpdateDataFromDto;
    }

    private void UpdateDataFromDto(bool success, GameStatisticsDto dto)
    {
        if (!success) return;
        if (dto == null) throw new ArgumentNullException(nameof(dto)); // dto не может быть null, если success == true

        SetTotalInAppTime(dto.TotalInAppTime);
        SetTotalLifeTime(dto.TotalLifeTime);
        SetGamesNumber(dto.GamesNumber);
        SetBestLifeTime(dto.BestLifeTime);
        SetBestScore(dto.BestScore);
    }
}
