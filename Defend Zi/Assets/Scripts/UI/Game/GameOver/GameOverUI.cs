using System;
using Desdiene.DataStorageFactories.DataContainers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.Types.Processes;
using Desdiene.UI.Elements;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
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
    private ISceneAsset _gameScene;
    private ISceneAsset _mainMenuScene;

    private IScoreAccessor _playerScore;
    private IDataContainer<IGameData> _storage;

    private GameDataSaver _gameDataSaver;

    private IHealthNotification _playerDeath;
    private IReincarnation _playerReincarnation;

    private IProcess _playerDeathPause;

    private IRewardedAd _rewardedAd;

    private bool doesPlayerBoughtRevival = false; // покупал ли игрок возрождение?

    [Inject]
    private void Constructor(ITime globalTime,
                         IDataContainer<IGameData> storage,
                         SceneLoader sceneLoader,
                         ScenesInBuild scenesInBuild,
                         IRewardedAd rewardedAd,
                         GameDataSaver gameDataSaver,
                         ComponentsProxy componentsProxy)
    {
        if (globalTime == null) throw new ArgumentNullException(nameof(globalTime));
        if (scenesInBuild == null) throw new ArgumentNullException(nameof(scenesInBuild));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gameDataSaver = gameDataSaver ?? throw new ArgumentNullException(nameof(gameDataSaver));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _playerDeathPause = globalTime.CreatePause(this, "Смерть игрока");
        _rewardedAd = rewardedAd ?? throw new ArgumentNullException(nameof(rewardedAd));

        _gameScene = SceneTypes.Game.Get(this, scenesInBuild);
        _mainMenuScene = SceneTypes.MainMenu.Get(this, scenesInBuild);

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
    private uint PlayerScore => _playerScore.Value;
    private uint PlayerBestScore => GameData.BestScore;

    private void SubscribeEvents()
    {
        _playerDeath.OnDeath += ((Desdiene.Types.Processes.IProcessMutator)_playerDeathPause).Start;
        _playerDeath.OnDeath += ChooseAndShowGameOverView;
        _playerReincarnation.OnReviving += ((Desdiene.Types.Processes.IProcessMutator)_playerDeathPause).Stop;

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
        _playerDeath.OnDeath -= ((Desdiene.Types.Processes.IProcessMutator)_playerDeathPause).Start;
        _playerDeath.OnDeath -= ChooseAndShowGameOverView;
        _playerReincarnation.OnReviving -= ((Desdiene.Types.Processes.IProcessMutator)_playerDeathPause).Stop;

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
