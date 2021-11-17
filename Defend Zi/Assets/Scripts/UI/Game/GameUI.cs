using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.Types.Processes;
using Desdiene.UI.Elements;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameView _gameView;
    [SerializeField, NotNull] private GamePauseView _gamePauseView;

    // Зависимость появления окна "adForRewardMessage" от события "возрождение за рекламу", описанное нажатием кнопки на "ReviveOfferView"
    private const float _adForRewardMessageShowTime = 1.5f;
    [SerializeField, NotNull] private ReviveOfferView _reviveOfferView;
    [SerializeField, NotNull] private UiElement _adForRewardMessageView;
    private ICoroutine _adForRewardMessageShowing;

    private IProcess _gamePause;
    private SceneLoader _sceneLoader;
    private ISceneAsset _mainMenuScene;

    private IReincarnationNotification _playerReincarnation;

    private IGameSettings _gameSettings;

    [Inject]
    private void Constructor(ITime globalTime,
                             SceneLoader sceneLoader,
                             ScenesInBuild scenesInBuild,
                             ComponentsProxy componentsProxy,
                             IGameSettings gameSettings)
    {
        if (globalTime == null) throw new ArgumentNullException(nameof(globalTime));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));

        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _gamePause = globalTime.CreatePause(this, "Подконтрольная игроку пауза игры");

        _mainMenuScene = SceneTypes.MainMenu.Get(this, scenesInBuild);

        _playerReincarnation = componentsProxy.PlayerReincarnation;
        _adForRewardMessageShowing = new CoroutineWrap(this);
        _gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    protected override void AwakeExt()
    {
        ShowSoundMuteState();
        SubscribeEvents();
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
        _reviveOfferView.OnReviveForAdClicked += ShowAdForRewardMessageView;
        _gamePauseView.OnSoundMuteChanged += SaveSoundMuteState;
    }

    private void UnsubscribeEvents()
    {
        _gameView.OnPauseClicked -= ShowGamePauseView;
        _gamePauseView.OnResumeClicked -= HideGamePauseView;
        _gamePauseView.OnMainMenuClicked -= LoadMainMenu;
        _playerReincarnation.OnReviving -= ShowGameView;
        _reviveOfferView.OnReviveForAdClicked -= ShowAdForRewardMessageView;
        _gamePauseView.OnSoundMuteChanged -= SaveSoundMuteState;
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
        ShowGameView();
    }

    private void ShowAdForRewardMessageView()
    {
        _adForRewardMessageShowing.StartContinuously(AdForRewardMessageShowing());
    }

    private IEnumerator AdForRewardMessageShowing()
    {
        _adForRewardMessageView.Show();
        yield return new WaitForSecondsRealtime(_adForRewardMessageShowTime);
        _adForRewardMessageView.Hide();
    }

    private void LoadMainMenu() => _sceneLoader.Load(_mainMenuScene);

    private void SaveSoundMuteState(bool mute)
    {
        _gameSettings.SetMuteState(mute);
    }

    private void ShowSoundMuteState()
    {
        _gamePauseView.SetSoundMutingToggleState(_gameSettings.SoundMuted);
    }
}
