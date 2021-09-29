using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using Desdiene.UI.Elements;
using UnityEngine;
using Zenject;

public class GameOverUI : MonoBehaviourExt
{
    [SerializeField, NotNull] private ReviveOfferView _reviveOfferView;
    [SerializeField, NotNull] private CollectRewardsOfferView _collectRewardsOfferView;
    [SerializeField, NotNull] private GameOverView _gameOverView;
    [SerializeField, NotNull] private ModalWindow _waitingView;
    [SerializeField, NotNull] private ModalError _errorView;

    private SceneLoader _sceneLoader;
    private SceneAsset _gameScene;
    private SceneAsset _mainMenuScene;

    private IScoreAccessor _playerScore;
    private IStorage<IGameData> _storage;

    private GameDataSaver _gameDataSaver;

    private IHealthNotification _playerDeath;
    private IReincarnation _playerReincarnation;

    private GlobalTimePause _playerDeathPause;

    private IRewardedAd _rewardedAd;

    private bool doesPlayerBoughtRevival = false; // покупал ли игрок возрождение?

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler,
                         IStorage<IGameData> storage,
                         SceneLoader sceneLoader,
                         IRewardedAd rewardedAd,
                         GameDataSaver gameDataSaver,
                         ComponentsProxy componentsProxy)
    {
        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gameDataSaver = gameDataSaver ?? throw new ArgumentNullException(nameof(gameDataSaver));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
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
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private IGameData GameData => _storage.GetData();
    private int PlayerScore => _playerScore.Value;
    private int PlayerBestScore => GameData.BestScore;

    private void SubscribeEvents()
    {
        _playerDeath.OnDeath += _playerDeathPause.Start;
        _playerDeath.OnDeath += ChooseAndShowGameOverView;
        _playerReincarnation.OnReviving += _playerDeathPause.Stop;

        _rewardedAd.OnFailedToShow += OnFailedToShowAd;
        _rewardedAd.OnFailedToShow += HideWaitingAndShowError;
        _rewardedAd.OnRewarded += ShowGameOverViewAndCollectRewards;
        _rewardedAd.OnClosed += HideWaitingView;

        _reviveOfferView.OnReviveForAdClicked += RevivePlayer;
        _reviveOfferView.OnRefuseToRevivingClicked += ShowGameOverViewAndCollectRewards;
        _reviveOfferView.OnMainMenuClicked += LoadMainMenuScene;

        _collectRewardsOfferView.OnCollectRewards += ShowAd;
        _collectRewardsOfferView.OnMainMenuClicked += LoadMainMenuScene;

        _gameOverView.OnReloadLvlClicked += LoadGameScene;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDeath -= _playerDeathPause.Start;
        _playerDeath.OnDeath -= ChooseAndShowGameOverView;
        _playerReincarnation.OnReviving -= _playerDeathPause.Stop;

        _rewardedAd.OnFailedToShow -= OnFailedToShowAd;
        _rewardedAd.OnFailedToShow -= HideWaitingAndShowError;
        _rewardedAd.OnRewarded -= ShowGameOverViewAndCollectRewards;
        _rewardedAd.OnClosed -= HideWaitingView;

        _reviveOfferView.OnReviveForAdClicked -= RevivePlayer;
        _reviveOfferView.OnRefuseToRevivingClicked -= ShowGameOverViewAndCollectRewards;
        _reviveOfferView.OnMainMenuClicked -= LoadMainMenuScene;

        _collectRewardsOfferView.OnCollectRewards -= ShowAd;
        _collectRewardsOfferView.OnMainMenuClicked -= LoadMainMenuScene;

        _gameOverView.OnReloadLvlClicked -= LoadGameScene;
    }

    private void ChooseAndShowGameOverView()
    {
        if (doesPlayerBoughtRevival) ShowCollectRewardsOfferView();
        else TryToShowReviveOfferView();
    }

    private void TryToShowReviveOfferView()
    {
        if (_rewardedAd.CanBeShown) ShowReviveOfferView();
        else ShowGameOverViewAndCollectRewards();
    }

    private void ShowGameOverViewAndCollectRewards()
    {
        CollectRewards();
        _gameOverView.Init(PlayerScore, PlayerBestScore);
        _gameOverView.Show();
    }

    private void ShowCollectRewardsOfferView()
    {
        _collectRewardsOfferView.Init(PlayerScore);
        _collectRewardsOfferView.Show();
    }

    private void ShowReviveOfferView()
    {
        _reviveOfferView.Init(PlayerScore);
        _reviveOfferView.Show();
    }

    private void ShowWaitingView() => _waitingView.Show();

    private void HideWaitingView() => _waitingView.Hide();

    private void HideWaitingAndShowError(string error)
    {
        _waitingView.Hide();
        //todo показать popUp с логом об ошибке
    }

    private void LoadGameScene() => _sceneLoader.Load(_gameScene);
    private void LoadMainMenuScene() => _sceneLoader.Load(_mainMenuScene);

    private void CollectRewards() => _gameDataSaver.CollectAndSaveGameData();

    private void ShowAd()
    {
        ShowWaitingView();
        _rewardedAd.Show();
    }

    private void OnFailedToShowAd(string error)
    {
        _errorView.Init($"Failed to show ad!\n{error}");
        _errorView.Show();
    }

    private void RevivePlayer()
    {
        doesPlayerBoughtRevival = true;
        _playerReincarnation.Revive();
    }
}
