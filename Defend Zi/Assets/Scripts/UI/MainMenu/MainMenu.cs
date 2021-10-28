using System;
using Desdiene.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviourExt
{
    [SerializeField, NotNull] private MainMenuView _mainMenuView;
    private GpgsLeaderboard _leaderboard;
    private ISceneAsset _gameScene;
    private SceneLoader _sceneLoader;
    private IGameSettings _gameSettings;

    [Inject]
    private void Constructor(SceneLoader sceneLoader,
                             ScenesInBuild scenesInBuild,
                             GpgsLeaderboard leaderboard,
                             GameSettings gameSettings)
    {
        if (scenesInBuild == null) throw new ArgumentNullException(nameof(scenesInBuild));
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _leaderboard = leaderboard ?? throw new ArgumentNullException(nameof(leaderboard));
        _gameScene = SceneTypes.Game.Get(this, scenesInBuild);
        _gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    protected override void AwakeExt()
    {
        _mainMenuView.SetSoundMutingToggleState(_gameSettings.SoundMuted);
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
        _mainMenuView.OnSoundMuteChanged += SetSoundMuteState;
    }

    private void UnsubscribeEvents()
    {
        _mainMenuView.OnGameClicked -= LoadGame;
        _mainMenuView.OnLeaderboardClicked -= OpenLeaderboard;
        _mainMenuView.OnSoundMuteChanged -= SetSoundMuteState;
    }

    private void LoadGame() => _sceneLoader.Load(_gameScene);

    private void OpenLeaderboard() => _leaderboard.Open();

    private void SetSoundMuteState(bool mute)
    {
        Debug.Log("ÊÐß Sound mute=" + mute);
        _gameSettings.SetMuteState(mute);
    }
}
