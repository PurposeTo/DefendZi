using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using SceneTypes;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviourExt
{
    [SerializeField, NotNull] private MainMenuView _mainMenuView;
    private SceneAsset _gameScene;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Constructor(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
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
    }

    private void UnsubscribeEvents()
    {
        _mainMenuView.OnGameClicked -= LoadGame;
    }

    private void LoadGame()
    {
        _sceneLoader.Load(_gameScene);
    }
}
