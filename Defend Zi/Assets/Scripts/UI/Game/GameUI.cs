using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;
    private GlobalTimePause _gamePause;
    private SceneLoader _sceneLoader;
    private SceneAsset _mainMenuScene;

    private IReincarnationNotification _playerReincarnation;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler,
                             SceneLoader sceneLoader,
                             ComponentsProxy componentsProxy)
    {
        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gamePause = new GlobalTimePause(this, globalTimeScaler, "Подконтрольная игроку пауза игры");

        _mainMenuScene = new SceneTypes.MainMenu(this);

        _playerReincarnation = componentsProxy.PlayerReincarnation;
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

    private void SubscribeEvents()
    {
        _gameView.OnPauseClicked += ShowGamePauseView;
        _gamePauseView.OnResumeClicked += HideGamePauseView;
        _gamePauseView.OnMainMenuClicked += LoadMainMenu;
        _playerReincarnation.OnReviving += ShowGameView;
    }

    private void UnsubscribeEvents()
    {
        _gameView.OnPauseClicked -= ShowGamePauseView;
        _gamePauseView.OnResumeClicked -= HideGamePauseView;
        _gamePauseView.OnMainMenuClicked -= LoadMainMenu;
        _playerReincarnation.OnReviving -= ShowGameView;
    }

    private void ShowGameView() => _gameView.Show();

    private void HideGameView() => _gameView.Hide();

    private void ShowGamePauseView()
    {
        _gamePause.Start();
        _gamePauseView.Show();
    }

    private void HideGamePauseView()
    {
        _gamePauseView.Hide();
        _gamePause.Stop();
    }

    private void LoadMainMenu() => _sceneLoader.Load(_mainMenuScene);

    private void SetDefaultState()
    {
        ShowGameView();
        HideGamePauseView();
    }
}
