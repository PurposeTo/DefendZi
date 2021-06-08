using System;
using Desdiene.MonoBehaviourExtention;
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
    private GlobalTimePauser IsGameOver;

    [Inject]
    public void Constructor(GlobalTimePausable globalTimePausable)
    {
        IsGameOver = new GlobalTimePauser(this, globalTimePausable, "Окончание игры");

        ComponentsProxy.OnInited += (componentsProxy) =>
        {
            SubscribeEvents(componentsProxy);
        };
    }

    public event Action OnGameOver;


    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents(ComponentsProxy componentsProxy)
    {
        componentsProxy.PlayerDeath.OnDied += EndGame;
    }

    private void UnsubscribeEvents()
    {
        ComponentsProxy.Instance.PlayerDeath.OnDied -= EndGame;
    }

    /// <summary>
    /// Закончить игру
    /// </summary>
    public void EndGame()
    {
        OnGameOver?.Invoke();
        IsGameOver.SetPause(true);
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    public void ResumeEndedGame()
    {
        IsGameOver.SetPause(false);
    }

    public void ReloadLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
