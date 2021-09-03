using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneTypes;
using Desdiene.TimeControls.Pauses;
using Desdiene.TimeControls.Scalers;
using SceneTypes;
using Zenject;

/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене. (не относится к сценам main menu и тп.)
/// Need to be a singleton!
/// </summary>
public class GameManager : MonoBehaviourExt
{
    private GlobalTimePause _isGameOver;
    private IDeath _playerDeath;
    private SceneAsset _gameScene;

    public event Action OnGameStarted;
    public event Action OnGameOver;

    [Inject]
    private void Constructor(GlobalTimeScaler timeScaler, ComponentsProxy componentsProxy)
    {
        _playerDeath = componentsProxy.PlayerDeath;
        _isGameOver = new GlobalTimePause(this, timeScaler, "Окончание игры");
        _gameScene = new Game(this);
        SubscribeEvents();
        OnGameStarted?.Invoke();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    public void ReloadLvl()
    {
        _gameScene.LoadAsSingle();
    }

    private void SubscribeEvents()
    {
        _playerDeath.OnDied += EndGame;
        _playerDeath.OnReborn += ResumeEndedGame;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= EndGame;
        _playerDeath.OnReborn -= ResumeEndedGame;
    }

    /// <summary>
    /// Закончить игру
    /// </summary>
    private void EndGame()
    {
        _isGameOver.Set(true);
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    private void ResumeEndedGame()
    {
        _isGameOver.Set(false);
    }
}
