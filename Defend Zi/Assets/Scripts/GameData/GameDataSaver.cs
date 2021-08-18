using Desdiene.GameDataAsset.Storage;
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

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy, GameManager gameManager, IStorage<IGameData> dataStorage)
    {
        _playerScore = componentsProxy.PlayerScore;
        _dataStorage = dataStorage;
        _gameManager = gameManager;

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
        SavePlayerScore();
        InvokeSavingData();
    }

    private void SavePlayerScore()
    {
        _dataStorage
            .GetData()
            .SetBestScore((uint)_playerScore.Value);
    }

    private void InvokeSavingData() => _dataStorage.InvokeSavingData();
}
