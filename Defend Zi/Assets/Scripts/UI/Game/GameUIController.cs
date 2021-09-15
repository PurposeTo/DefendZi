using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameOverView _gameOverView;
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;
    private GlobalTimePause _gamePause;
    private SceneLoader _sceneLoader;
    private SceneAsset _mainMenu;
    private GameManager _gameManager;
    private IStorage<IGameData> _storage;
    private IDeath _playerDeath;
    private IScoreGetter _playerScore;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler,
                             IStorage<IGameData> storage,
                             SceneLoader sceneLoader,
                             ComponentsProxy componentsProxy,
                             GameManager gameManager)
    {
        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (sceneLoader == null) throw new ArgumentNullException(nameof(sceneLoader));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _gamePause = new GlobalTimePause(this, globalTimeScaler, "Подконтрольная игроку пауза игры");
        _playerDeath = componentsProxy.PlayerDeath;
        _playerScore = componentsProxy.PlayerScore;
        SetDefaultState();
        SubscribeEvents();
    }

    private IGameData GameData => _storage.GetData();

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _playerDeath.OnDied += EnableGameOverView;
        _gameView.OnPauseClicked += EnableGamePauseView;
        _gamePauseView.OnResumeClicked += DisableGamePauseView;
        _gamePauseView.OnMainMenuClicked += LoadMainMenu;
        _gameOverView.OnReloadLvlClicked += ReloadLvl;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= EnableGameOverView;
        _gameView.OnPauseClicked -= EnableGamePauseView;
        _gamePauseView.OnResumeClicked -= DisableGamePauseView;
        _gamePauseView.OnMainMenuClicked -= LoadMainMenu;
        _gameOverView.OnReloadLvlClicked -= ReloadLvl;
    }

    private void EnableGameView()
    {
        _gameView.Enable();
    }

    private void DisableGameView()
    {
        _gameView.Disable();
    }

    private void EnableGamePauseView()
    {
        _gamePause.Start();
        _gamePauseView.Enable();
    }

    private void DisableGamePauseView()
    {
        _gamePauseView.Disable();
        _gamePause.Complete();
    }

    private void EnableGameOverView()
    {
        _gameOverView.Enable(GameData.BestScore, _playerScore.Value);
    }

    private void DisableGameOverView()
    {
        _gameOverView.Disable();
    }

    private void LoadMainMenu()
    {
        _sceneLoader.Load(_mainMenu);
    }

    private void ReloadLvl()
    {
        _gameManager.ReloadLvl();
    }

    private void SetDefaultState()
    {
        EnableGameView();
        DisableGameOverView();
    }
}
