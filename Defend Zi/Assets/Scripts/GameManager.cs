using System;
using Desdiene.GameDataAsset.Storage;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControl.Pausable;
using Desdiene.TimeControl.Pauser;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене. (не относится к сценам main menu и тп.)
/// Need to be a singleton!
/// </summary>
public class GameManager : MonoBehaviourExt
{
    private GlobalTimePauser _isGameOver;

    private IDeath _playerDeath;

    public event Action OnGameStarted;
    public event Action OnGameOver;

    [Inject]
    private void Constructor(GlobalTimePausable globalTimePausable, ComponentsProxy componentsProxy, IStorage<IGameData> dataStorage)
    {
        _playerDeath = componentsProxy.PlayerDeath;

        _isGameOver = new GlobalTimePauser(this, globalTimePausable, "Окончание игры");
        SubscribeEvents();
        OnGameStarted?.Invoke();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    public void ReloadLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        _isGameOver.SetPause(true);
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    private void ResumeEndedGame()
    {
        _isGameOver.SetPause(false);
    }
}
