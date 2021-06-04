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

        PlayerHealth.OnInited += (playerHealth) =>
        {
            SubscribeEvents(playerHealth);
        };
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents(PlayerHealth playerHealth)
    {
        playerHealth.OnDied += EndGame;
    }

    private void UnsubscribeEvents()
    {
        PlayerHealth.Instance.OnDied -= EndGame;
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
