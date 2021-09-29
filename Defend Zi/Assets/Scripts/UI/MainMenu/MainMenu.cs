using System;
using Desdiene.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using SceneTypes;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviourExt
{
    [SerializeField, NotNull] private MainMenuView _mainMenuView;
    private GPGSLeaderboard _leaderboard;
    private SceneAsset _gameScene;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Constructor(SceneLoader sceneLoader, GPGSLeaderboard leaderboard)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _leaderboard = leaderboard ?? throw new ArgumentNullException(nameof(leaderboard));
        _gameScene = new Game(this);
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
    }

    private void UnsubscribeEvents()
    {
        _mainMenuView.OnGameClicked -= LoadGame;
        _mainMenuView.OnLeaderboardClicked -= OpenLeaderboard;
    }

    private void LoadGame() => _sceneLoader.Load(_gameScene);

    private void OpenLeaderboard() => _leaderboard.Open();
}
