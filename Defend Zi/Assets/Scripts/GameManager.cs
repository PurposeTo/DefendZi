using System;
using Desdiene.Singleton.Unity;
using Desdiene.TimeControl.Pause;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене. (не относится к сценам main menu и тп.)
/// </summary>
public class GameManager : SceneSingleton<GameManager>
{
    public event Action OnGameOver;
    private PausableGlobalTime IsGameOver;

    protected override void AwakeSingleton()
    {
        GlobalTimePauser.OnInited += (globalTimePauser) =>
        {
            IsGameOver = new PausableGlobalTime(this, globalTimePauser, "Окончание игры");
        };

        ComponentsProxy.OnInited += (componentsProxy) =>
        {
            SubscribeEvents(componentsProxy);
        };
    }

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
