using System;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Класс отвечает за сохранение данных после окончания игры.
/// Need to be a scene singleton!
/// </summary>
public class GameOverDataSaver : MonoBehaviourExt
{
    private GameStatistics _gameStatistics;
    private IScoreAccessor _playerScore;
    private PlayerLifeTime _playerLifeTime;

    [Inject]
    private void Constructor(GameStatistics gameStatistics,
                             ComponentsProxy componentsProxy)
    {
        _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _playerScore = componentsProxy.PlayerScore;
        _playerLifeTime = componentsProxy.PlayerLifeTime;
    }

    public void CollectAndSave()
    {
        TimeSpan playerLifeTime = _playerLifeTime.Value;
        uint playerScore = _playerScore.Value;
        _gameStatistics.AddLifeTime(playerLifeTime);
        _gameStatistics.IncrementGamesNumber();
        _gameStatistics.SetBestLifeTime(playerLifeTime);
        _gameStatistics.SetBestScore(playerScore);
        _gameStatistics.Save();
    }
}
