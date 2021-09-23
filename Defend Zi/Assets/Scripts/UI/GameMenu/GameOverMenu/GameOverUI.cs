using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using UnityEngine;
using Zenject;

public class GameOverUI : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameOverView _gameOverView;
    [SerializeField, NotNull] private CollectRewardsOfferView _collectRewardsOfferView;
    [SerializeField, NotNull] private ReviveOfferView _reviveOfferView;

    private SceneLoader _sceneLoader;
    private SceneAsset _gameScene;

    private IScoreAccessor _playerScore;
    private IStorage<IGameData> _storage;

    private GameDataSaver _gameDataSaver;

    private IDeath _playerDeath;
    private IReincarnation _playerReincarnation;

    private GlobalTimePause _playerDeathPause;

    private IRewardedAd _rewardedAd;

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
        _playerDeathPause = new GlobalTimePause(this, globalTimeScaler, "Смерть игрока");
        _rewardedAd = rewardedAd ?? throw new ArgumentNullException(nameof(rewardedAd));

        _gameScene = new SceneTypes.Game(this);

        _playerDeath = componentsProxy.PlayerDeath;
        _playerReincarnation = componentsProxy.PlayerReincarnation;
        _playerScore = componentsProxy.PlayerScore;
    }

    protected override void AwakeExt()
    {
        SubscribeEvents();
        SetDefaultState();
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
        _playerDeath.OnDied += _playerDeathPause.Start;
        _playerDeath.OnDied += ShowGameOverViewAndCollectRewards;
        _playerReincarnation.OnRevived += _playerDeathPause.Stop;
        _playerReincarnation.OnRevived += HideGameOverView;

        _gameOverView.OnReloadLvlClicked += LoadGameScene;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= _playerDeathPause.Start;
        _playerDeath.OnDied -= ShowGameOverViewAndCollectRewards;
        _playerReincarnation.OnRevived -= _playerDeathPause.Stop;
        _playerReincarnation.OnRevived -= HideGameOverView;

        _gameOverView.OnReloadLvlClicked -= LoadGameScene;
    }

    private void ShowGameOverViewAndCollectRewards()
    {
        _gameOverView.Show(PlayerScore, PlayerBestScore);
        CollectRewards();
    }

    private void HideGameOverView() => _gameOverView.Hide();


    private void ShowCollectRewardsOfferView()
    {
        _collectRewardsOfferView.Show(PlayerScore);
    }

    private void HideCollectRewardsOfferView()
    {
        _collectRewardsOfferView.Hide();
    }

    private void ShowReviveOfferView()
    {
        _reviveOfferView.Show(PlayerScore);
    }

    private void HideReviveOfferView()
    {
        _reviveOfferView.Hide();
    }

    private void LoadGameScene() => _sceneLoader.Load(_gameScene);

    private void CollectRewards() => _gameDataSaver.SaveGameData();

    private void ShowAd() => _rewardedAd.Show();

    private void SetDefaultState()
    {
        HideGameOverView();
        HideCollectRewardsOfferView();
        HideReviveOfferView();
    }
}
