using Desdiene.DataStorageFactories.Storages;
using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviour
{
    private GameManager _gameManager;
    private IStorage<IGameData> _storage;
    private IDeath _playerDeath;
    private IScoreGetter _playerScore;

    [Inject]
    private void Constructor(GameManager gameManager, ComponentsProxy componentsProxy, IStorage<IGameData> storage)
    {
        _gameManager = gameManager;
        _storage = storage;
        _playerDeath = componentsProxy.PlayerDeath;
        _playerScore = componentsProxy.PlayerScore;
        InitViews();
        SubscribeEvents();
    }

    [SerializeField, NotNull] private GameOverView _gameOverView;

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _playerDeath.OnDied += _gameOverView.Enable;
        _playerDeath.OnDied += SetBestScore;
        _playerDeath.OnDied += SetScore;
        _gameOverView.OnReloadLvlClicked += _gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= _gameOverView.Enable;
        _playerDeath.OnDied -= SetBestScore;
        _playerDeath.OnDied -= SetScore;
        _gameOverView.OnReloadLvlClicked -= _gameManager.ReloadLvl;
    }

    private void SetBestScore()
    {
        _gameOverView.SetBestScore(_storage.GetData().BestScore);
    }

    private void SetScore()
    {
        _gameOverView.SetScore(_playerScore.Value);
    }

    private void InitViews()
    {
        _gameOverView.Enable(); // Включить UI для первоначальной отрисовки и кеширования отрендеренных данных
        _gameOverView.Disable();
    }

}
