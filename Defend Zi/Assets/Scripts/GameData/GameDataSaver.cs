using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Класс отвечает за сохранение данных с игровой сцены. (не относится к сценам main menu и тп.)
/// Need to be a scene singleton!
/// </summary>
public class GameDataSaver : MonoBehaviourExt
{
    private IStorage<IGameData> _dataStorage;

    private IScoreAccessor _playerScore;
    private GameStatisticsCollector _statisticsCollector;
    private GameStatistics Statistics => _statisticsCollector.GetStatistics();

    [Inject]
    private void Constructor(IStorage<IGameData> dataStorage,
                             ComponentsProxy componentsProxy,
                             GameStatisticsCollector statisticsCollector)
    {
        if (componentsProxy is null) throw new ArgumentNullException(nameof(componentsProxy));

        _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

        _playerScore = componentsProxy.PlayerScore;
        _statisticsCollector = statisticsCollector ?? throw new ArgumentNullException(nameof(statisticsCollector));
    }

    public void CollectAndSaveGameData()
    {
        CollectGameData();
        InvokeSavingData();
    }

    private void CollectGameData()
    {
        CollectGamesNumber();
        CollectPlayerScore();
        CollectPlayerLifeTime();
    }

    private void CollectGamesNumber() => _dataStorage.GetData().IncreaseGamesNumber();

    private void CollectPlayerScore()
    {
        _dataStorage
            .GetData()
            .SetBestScore((uint)_playerScore.Value);
    }

    private void CollectPlayerLifeTime()
    {
        uint lifeTimeSec = (uint)Statistics.LifeTimeSec;

        _dataStorage
            .GetData()
            .SetAverageLifeTimeSec(lifeTimeSec);

        _dataStorage
            .GetData()
            .SetBestLifeTimeSec(lifeTimeSec);
    }

    private void InvokeSavingData() => _dataStorage.InvokeSavingData();
}
