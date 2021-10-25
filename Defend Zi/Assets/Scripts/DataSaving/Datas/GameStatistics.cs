using System;
using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Статистика игры.
/// Класс сделан MonoBehaviour для возможности чтения полей через инспектор.
/// </summary>
public class GameStatistics : MonoBehaviourExt, IGameStatisticsAccessorNotifier
{
    private IStorageAsync<GameStatisticsDto> _storage;
    // todo добавить события об изменении
    private TimeSpan _totalLifeTime = TimeSpan.Zero;
    private uint _gamesNumber = 0;
    private uint _bestScore = 0;
    private TimeSpan _bestLifeTime = TimeSpan.Zero;

    [Inject]
    private void Constructor(IStorageAsync<GameStatisticsDto> storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _storage.Read(UpdateDataFromDto);
    }

    TimeSpan IGameStatisticsAccessorNotifier.TotalLifeTime => _totalLifeTime;
    uint IGameStatisticsAccessorNotifier.GamesNumber => _gamesNumber;
    uint IGameStatisticsAccessorNotifier.BestScore => _bestScore;
    TimeSpan IGameStatisticsAccessorNotifier.BestLifeTime => _bestLifeTime;

    public void Save()
    {
        var dto = new GameStatisticsDto()
        {
            TotalLifeTime = _totalLifeTime,
            GamesNumber = _gamesNumber,
            BestLifeTime = _bestLifeTime,
            BestScore = _bestScore,
        };

        _storage.Update(dto, (_) => { });
    }

    public void AddLifeTime(TimeSpan value) => SetTotalLifeTime(_totalLifeTime + value);

    public void SetTotalLifeTime(TimeSpan value)
    {
        _totalLifeTime = value;
    }

    public void IncrementGamesNumber() => SetGamesNumber(_gamesNumber + 1);

    public void SetGamesNumber(uint value)
    {
        _gamesNumber = value;
    }

    public void SetBestLifeTime(TimeSpan value)
    {
        if (value <= _bestLifeTime) return;

        _bestLifeTime = value;
    }

    public void SetBestScore(uint value)
    {
        if (value <= _bestScore) return;

        _bestScore = value;
    }

    private void UpdateDataFromDto(bool success, GameStatisticsDto dto)
    {
        if (!success) return;
        if (dto == null) throw new ArgumentNullException(nameof(dto)); // dto не может быть null, если success == true

        SetTotalLifeTime(dto.TotalLifeTime);
        SetGamesNumber(dto.GamesNumber);
        SetBestLifeTime(dto.BestLifeTime);
        SetBestScore(dto.BestScore);
    }
}
