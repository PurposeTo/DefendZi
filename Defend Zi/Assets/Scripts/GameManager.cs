using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
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
    private GlobalTimePause _gameOverPause;
    private IDeath _playerDeath;
    private SceneLoader _singleSceneLoader;

    public event Action OnGameStarted;
    public event Action OnGameOver;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler, ComponentsProxy componentsProxy, SceneLoader singleSceneLoader)
    {
        if (globalTimeScaler == null) throw new ArgumentNullException(nameof(globalTimeScaler));
        if (componentsProxy == null) throw new ArgumentNullException(nameof(componentsProxy));
        if (singleSceneLoader == null) throw new ArgumentNullException(nameof(singleSceneLoader));

        _gameOverPause = new GlobalTimePause(this, globalTimeScaler, "Окончание игры");
        _playerDeath = componentsProxy.PlayerDeath;
        _singleSceneLoader = singleSceneLoader;
        SubscribeEvents();
        OnGameStarted?.Invoke();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    public void ReloadLvl()
    {
        _singleSceneLoader.Reload();
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
        _gameOverPause.Start();
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    private void ResumeEndedGame()
    {
        _gameOverPause.Complete();
    }
}
