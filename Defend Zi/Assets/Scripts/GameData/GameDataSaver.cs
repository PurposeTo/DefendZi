using System;
using Desdiene.DataSaving.Storages;
using Desdiene.MonoBehaviourExtension;
using Zenject;

/// <summary>
/// Класс отвечает за сохранение данных с игровой сцены. (не относится к сценам main menu и тп.)
/// Need to be a scene singleton!
/// </summary>
public class GameDataSaver : MonoBehaviourExt
{
    private IStorageAsync<SavableDataAsync> _dataStorage;

    private IScoreAccessor _playerScore;
    private GameStatisticsCollector _statisticsCollector;
    private GameStatistics Statistics => _statisticsCollector.GetStatistics();

    [Inject]
    private void Constructor(IStorageAsync<SavableDataAsync> dataStorage,
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
        var data = CollectGameData();
        _dataStorage.Save(data, (_) => { });
    }

    private SavableDataAsync CollectGameData()
    {
        // todo нужно сохранить данные, основываясь на тех, которые уже были: лучший рекорд, кол-во игровых попыток и тп.
        uint playerScore = _playerScore.Value;
        TimeSpan playerLifeTime = Statistics.LifeTime;

        return new SavableDataAsync()
        {


        };
    }
}
