using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using SceneTypes;
using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviourExt
{
   // [SerializeField, NotNull] private PlayerDeathView _playerDeathView;
    [SerializeField, NotNull] private GameOverView _gameOverView;
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;
    private GlobalTimePause _gamePause;
    private SceneLoader _sceneLoader;
    private SceneAsset _mainMenu;
    private GameManager _gameManager;
    private IStorage<IGameData> _storage;
    private IRewardedAd _rewardedAd;
    private IReincarnation _playerReincarnation;
    private IScoreAccessor _playerScore;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler,
                             IStorage<IGameData> storage,
                             SceneLoader sceneLoader,
                             ComponentsProxy componentsProxy,
                             GameManager gameManager)
    {
        IRewardedAd rewardedAd = new SuccessRewardedAd(); // todo убрать заглушку

        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _mainMenu = new MainMenu(this);
        _gamePause = new GlobalTimePause(this, globalTimeScaler, "Подконтрольная игроку пауза игры");
        _rewardedAd = rewardedAd ?? throw new ArgumentNullException(nameof(rewardedAd));
        _playerReincarnation = componentsProxy.PlayerReincarnation;
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
        _gameManager.OnGameOver += EnableGameOverView;
        _gameView.OnPauseClicked += EnableGamePauseView;
        _gamePauseView.OnResumeClicked += DisableGamePauseView;
        _gamePauseView.OnMainMenuClicked += LoadMainMenu;
        _gameOverView.OnReloadLvlClicked += ReloadLvl;
    }

    private void UnsubscribeEvents()
    {
        _gameManager.OnGameOver -= EnableGameOverView;
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

    private void EnablePlayerDeathView()
    {
     //   _playerDeathView.Enable(_playerScore.Value);
    }

    private void DisablePlayerDeathView()
    {
       // _playerDeathView.Disable();
    }

    private void RevivePlayer()
    {
        _playerReincarnation.Revive();
    }

    private void EnableGameOverView()
    {
        _gameOverView.Enable(_playerScore.Value, GameData.BestScore);
    }

    private void DisableGameOverView()
    {
        _gameOverView.Disable();
    }

    private void ShowAd()
    {
        _rewardedAd.Show();
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
        DisablePlayerDeathView();
        DisableGameOverView();
        DisableGamePauseView();
    }
}
