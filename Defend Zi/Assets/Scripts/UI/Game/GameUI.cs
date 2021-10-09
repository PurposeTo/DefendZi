using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;
    private IProcess _gamePause;
    private SceneLoader _sceneLoader;
    private ISceneAsset _mainMenuScene;

    private IReincarnationNotification _playerReincarnation;

    [Inject]
    private void Constructor(ITime globalTime,
                             SceneLoader sceneLoader,
                             ScenesInBuild scenesInBuild,
                             ComponentsProxy componentsProxy)
    {
        if (globalTime == null) throw new ArgumentNullException(nameof(globalTime));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gamePause = globalTime.CreatePause(this, "Подконтрольная игроку пауза игры");

        _mainMenuScene = SceneTypes.MainMenu.Get(this, scenesInBuild);

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
        Debug.Log($"KEEEk _gameView: {_gameView}, _gamePauseView: {_gamePauseView}, _playerReincarnation: {_playerReincarnation}");
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
        ((Desdiene.Types.Processes.IProcessMutator)_gamePause).Start();
        _gamePauseView.Show();
    }

    private void HideGamePauseView()
    {
        _gamePauseView.Hide();
        ((Desdiene.Types.Processes.IProcessMutator)_gamePause).Stop();
    }

    private void LoadMainMenu() => _sceneLoader.Load(_mainMenuScene);

    private void SetDefaultState()
    {
        ShowGameView();
        HideGamePauseView();
    }
}
