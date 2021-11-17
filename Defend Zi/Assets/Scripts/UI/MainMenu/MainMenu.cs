using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviourExt
{
    [SerializeField, NotNull] private MainMenuView _mainMenuView;
    private GpgsLeaderboardMono _leaderboard;
    private ISceneAsset _gameScene;
    private SceneLoader _sceneLoader;
    private IGameSettings _gameSettings;
    private IGameStatisticsAccessorNotifier _gameStatistics;

    [Inject]
    private void Constructor(SceneLoader sceneLoader,
                             ScenesInBuild scenesInBuild,
                             GpgsLeaderboardMono leaderboard,
                             IGameSettings gameSettings,
                             IGameStatistics gameStatistics)
    {
        if (scenesInBuild == null) throw new ArgumentNullException(nameof(scenesInBuild));
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _leaderboard = leaderboard ?? throw new ArgumentNullException(nameof(leaderboard));
        _gameScene = SceneTypes.Game.Get(this, scenesInBuild);
        _gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
    }

    protected override void AwakeExt()
    {
        ShowSoundMuteState();
        ShowBestScore();
        ShowBestLifeTime();
        ShowAverageLifeTime();
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _mainMenuView.OnGameClicked += LoadGame;
        _mainMenuView.OnLeaderboardClicked += OpenLeaderboard;
        _mainMenuView.OnSoundMuteChanged += SaveSoundMuteState;

        _gameStatistics.OnBestScoreChanged += ShowBestScore;
        _gameStatistics.OnBestLifeTimeChanged += ShowBestLifeTime;
        _gameStatistics.OnAverageLifeTimeChanged += ShowAverageLifeTime;
    }

    private void UnsubscribeEvents()
    {
        _mainMenuView.OnGameClicked -= LoadGame;
        _mainMenuView.OnLeaderboardClicked -= OpenLeaderboard;
        _mainMenuView.OnSoundMuteChanged -= SaveSoundMuteState;
    }

    private void LoadGame() => _sceneLoader.Load(_gameScene);

    private void OpenLeaderboard() => _leaderboard.UpdateScoreAndOpen();

    private void SaveSoundMuteState(bool mute)
    {
        _gameSettings.SetMuteState(mute);
    }

    private void ShowSoundMuteState()
    {
        _mainMenuView.SetSoundMutingToggleState(_gameSettings.SoundMuted);
    }

    private void ShowBestScore()
    {
        _mainMenuView.SetBestScore(_gameStatistics.BestScore);
    }

    private void ShowBestLifeTime()
    {
        _mainMenuView.SetBestLifeTime(_gameStatistics.BestLifeTime);
    }
    private void ShowAverageLifeTime()
    {
        _mainMenuView.SetAverageLifeTime(_gameStatistics.AverageLifeTime);
    }

}
