using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using UnityEngine;
using Zenject;

public class GameMenu : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameOverView _gameOverView;
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;
    private GlobalTimePause _gamePause;
    private GlobalTimePause _playerDeathPause;
    private SceneLoader _sceneLoader;
    private SceneAsset _gameScene;
    private SceneAsset _mainMenuScene;
    private GameDataSaver _gameDataSaver;
    private IStorage<IGameData> _storage;
    private IRewardedAd _rewardedAd;
    private IDeath _playerDeath;
    private IReincarnation _playerReincarnation;
    private IScoreAccessor _playerScore;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler,
                             IStorage<IGameData> storage,
                             SceneLoader sceneLoader,
                             GameDataSaver gameDataSaver,
                             ComponentsProxy componentsProxy)
    {
        IRewardedAd rewardedAd = new SuccessRewardedAd(); // todo убрать заглушку

        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gameDataSaver = gameDataSaver ?? throw new ArgumentNullException(nameof(gameDataSaver));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _gamePause = new GlobalTimePause(this, globalTimeScaler, "Подконтрольная игроку пауза игры");
        _playerDeathPause = new GlobalTimePause(this, globalTimeScaler, "Смерть игрока");
        _rewardedAd = rewardedAd ?? throw new ArgumentNullException(nameof(rewardedAd));

        _gameScene = new SceneTypes.Game(this);
        _mainMenuScene = new SceneTypes.MainMenu(this);

        _playerDeath = componentsProxy.PlayerDeath;
        _playerReincarnation = componentsProxy.PlayerReincarnation;
        _playerScore = componentsProxy.PlayerScore;
    }

    protected override void AwakeExt()
    {
        SetDefaultState();
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private IGameData GameData => _storage.GetData();

    private void SubscribeEvents()
    {
        _playerDeath.OnDied += _playerDeathPause.Start;
        _playerDeath.OnDied += EnableGameOverView;
        _playerReincarnation.OnRevived += _playerDeathPause.Complete;

        _gameView.OnPauseClicked += EnableGamePauseView;
        _gamePauseView.OnResumeClicked += DisableGamePauseView;
        _gamePauseView.OnMainMenuClicked += LoadMainMenu;
        _gameOverView.OnReloadLvlClicked += LoadGameScene;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= _playerDeathPause.Start;
        _playerDeath.OnDied -= EnableGameOverView;
        _playerReincarnation.OnRevived -= _playerDeathPause.Complete;

        _gameView.OnPauseClicked -= EnableGamePauseView;
        _gamePauseView.OnResumeClicked -= DisableGamePauseView;
        _gamePauseView.OnMainMenuClicked -= LoadMainMenu;
        _gameOverView.OnReloadLvlClicked -= LoadGameScene;
    }

    private void EnableGameView() => _gameView.Enable();

    private void DisableGameView() => _gameView.Disable();

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

    private void RevivePlayer() => _playerReincarnation.Revive();

    private void CollectRewards() => _gameDataSaver.SaveGameData();

    private void EnableGameOverView()
    {
        _gameOverView.Enable(_playerScore.Value, GameData.BestScore);
        CollectRewards(); // данный метод должен вызываться здесь?
    }

    private void DisableGameOverView() => _gameOverView.Disable();

    private void ShowAd() => _rewardedAd.Show();

    private void LoadMainMenu() => _sceneLoader.Load(_mainMenuScene);

    private void LoadGameScene() => _sceneLoader.Load(_gameScene);

    private void SetDefaultState()
    {
        EnableGameView();
        DisableGameOverView();
        DisableGamePauseView();
    }
}
