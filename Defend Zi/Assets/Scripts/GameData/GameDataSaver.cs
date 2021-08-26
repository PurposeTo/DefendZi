using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Класс отвечает за сохранение данных с игровой сцены. (не относится к сценам main menu и тп.)
/// Need to be a singleton!
/// </summary>
public class GameDataSaver : MonoBehaviourExt
{
    private IStorage<IGameData> _dataStorage;
    private GameManager _gameManager;

    private IScoreGetter _playerScore;
    private GameStatisticsCollector _statisticsCollector;
    private GameStatistics Statistics => _statisticsCollector.GetStatistics();

    [Inject]
    private void Constructor(IStorage<IGameData> dataStorage,
                             GameManager gameManager,
                             ComponentsProxy componentsProxy,
                             GameStatisticsCollector statisticsCollector)
    {
        _dataStorage = dataStorage;
        _gameManager = gameManager;

        _playerScore = componentsProxy.PlayerScore;
        _statisticsCollector = statisticsCollector;

        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _gameManager.OnGameOver += SaveGameData;
    }

    private void UnsubscribeEvents()
    {
        _gameManager.OnGameOver -= SaveGameData;
    }

    private void SaveGameData()
    {
        SaveGamesNumber();
        SavePlayerScore();
        SavePlayerLifeTime();
        InvokeSavingData();
    }

    private void SaveGamesNumber() => _dataStorage.GetData().IncreaseGamesNumber();

    private void SavePlayerScore()
    {
        _dataStorage
            .GetData()
            .SetBestScore((uint)_playerScore.Value);
    }

    private void SavePlayerLifeTime()
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
