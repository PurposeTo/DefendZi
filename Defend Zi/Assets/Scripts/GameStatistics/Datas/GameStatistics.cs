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
    private TimeSpan _averageLifeTime = TimeSpan.Zero;
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

    private event Action OnTotalInAppTimeChanged;
    private event Action OnTotalLifeTimeChanged;
    private event Action OnAverageLifeTimeChanged;
    private event Action OnGamesNumberChanged;
    private event Action OnBestScoreChanged;
    private event Action OnBestLifeTimeChanged;


    event Action IGameStatisticsNotifier.OnTotalInAppTimeChanged
    {
        add => OnTotalInAppTimeChanged += value;
        remove => OnTotalInAppTimeChanged -= value;
    }

    event Action IGameStatisticsNotifier.OnTotalLifeTimeChanged
    {
        add => OnTotalLifeTimeChanged += value;
        remove => OnTotalLifeTimeChanged -= value;
    }

    event Action IGameStatisticsNotifier.OnAverageLifeTimeChanged
    {
        add => OnAverageLifeTimeChanged += value;
        remove => OnAverageLifeTimeChanged -= value;
    }

    event Action IGameStatisticsNotifier.OnGamesNumberChanged
    {
        add => OnGamesNumberChanged += value;
        remove => OnGamesNumberChanged -= value;
    }

    event Action IGameStatisticsNotifier.OnBestScoreChanged
    {
        add => OnBestScoreChanged += value;
        remove => OnBestScoreChanged -= value;
    }

    event Action IGameStatisticsNotifier.OnBestLifeTimeChanged
    {
        add => OnBestLifeTimeChanged += value;
        remove => OnBestLifeTimeChanged -= value;
    }

    TimeSpan IGameStatisticsAccessor.TotalInAppTime => _totalInAppTime;
    TimeSpan IGameStatisticsAccessor.TotalLifeTime => _totalLifeTime;
    TimeSpan IGameStatisticsAccessor.AverageLifeTime => _averageLifeTime;
    uint IGameStatisticsAccessor.GamesNumber => _gamesNumber;
    uint IGameStatisticsAccessor.BestScore => _bestScore;
    TimeSpan IGameStatisticsAccessor.BestLifeTime => _bestLifeTime;
    void ISavableData.Save() => Save();
    void IGameStatisticsMutator.AddLifeTime(TimeSpan value) => AddLifeTime(value);
    void IGameStatisticsMutator.IncrementGamesNumber() => IncrementGamesNumber();
    void IGameStatisticsMutator.SetBestLifeTime(TimeSpan value) => SetBestLifeTime(value);
    void IGameStatisticsMutator.SetBestScore(uint value) => SetBestScore(value);

    private void AddTotalInAppTime(TimeSpan value) => SetTotalInAppTime(_totalInAppTime + value);

    private void SetTotalInAppTime(TimeSpan value)
    {
        _totalInAppTime = value;
        OnTotalInAppTimeChanged?.Invoke();
    }

    private void AddLifeTime(TimeSpan value) => SetTotalLifeTime(_totalLifeTime + value);

    private void SetTotalLifeTime(TimeSpan value)
    {
        if (_totalLifeTime == value) return;

        _totalLifeTime = value;
        OnTotalLifeTimeChanged?.Invoke();
        SetAverageLifeTime(_totalLifeTime, _gamesNumber);
    }

    private void SetAverageLifeTime(TimeSpan totalLifeTime, uint gamesNumber)
    {
        if (gamesNumber == 0) return;
        double milliseconds = totalLifeTime.TotalMilliseconds / gamesNumber;
        TimeSpan averageLifeTime = TimeSpan.FromMilliseconds(milliseconds);
        if (averageLifeTime == totalLifeTime) return;

        _averageLifeTime = averageLifeTime;
        OnAverageLifeTimeChanged?.Invoke();
    }

    private void IncrementGamesNumber() => SetGamesNumber(_gamesNumber + 1);

    private void SetGamesNumber(uint value)
    {
        if (_gamesNumber == value) return;

        _gamesNumber = value;
        OnGamesNumberChanged?.Invoke();
        SetAverageLifeTime(_totalLifeTime, _gamesNumber);
    }

    private void SetBestLifeTime(TimeSpan value)
    {
        if (value <= _bestLifeTime) return;

        _bestLifeTime = value;
        OnBestLifeTimeChanged?.Invoke();
    }

    private void SetBestScore(uint value)
    {
        if (value <= _bestScore) return;

        _bestScore = value;
        OnBestScoreChanged?.Invoke();
    }

    private void SubscribeEvents()
    {
        _storage.OnReaded += UpdateDataFromDto;
    }

    private void UnsubscribeEvents()
    {
        _storage.OnReaded -= UpdateDataFromDto;
    }

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
